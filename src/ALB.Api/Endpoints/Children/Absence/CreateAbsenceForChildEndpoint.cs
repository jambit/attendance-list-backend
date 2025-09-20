using System.Runtime.InteropServices.JavaScript;
using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using ALB.Domain.Values;
using FastEndpoints;
using NodaTime;

namespace ALB.Api.Endpoints.Children.Absence;

public class CreateAbsenceForChildEndpoint(IAbsenceDayRepository absenceRepo) : Endpoint<CreateAbsenceRequest, CreateAbsenceResponse>
{
    public override void Configure()
    {
        Post("/api/children/{childId:guid}/absence");
        Policies(SystemRoles.AdminPolicy);
        Policies(SystemRoles.ParentPolicy);
    }

    public override async Task HandleAsync(CreateAbsenceRequest request, CancellationToken ct)
    {
        var childId = Route<Guid>("ChildId");
        
        var startDate = LocalDate.FromDateTime(request.StartDate);
        var endDate = LocalDate.FromDateTime(request.EndDate);

        if (startDate > endDate)
        {
            await SendAsync(new CreateAbsenceResponse("Start date cannot be after end date."), 400, ct);
            return;
        }

        var alreadyExists = await absenceRepo.ExistsInRangeAsync(childId, startDate, endDate, ct);
        
        if (alreadyExists)
        {
            await SendAsync(new CreateAbsenceResponse("An absence already exists for one or more days in this date range."), 409, ct);

            return;
        }

        var absencesToCreate = new List<AbsenceDay>();
        for (var date = startDate; date <= endDate; date = date.PlusDays(1))
        {
            var absence = new AbsenceDay
            {
               
                ChildId = childId,
                Date = date,
                AbsenceStatusId = request.AbsenceStatusId,
            };
            absencesToCreate.Add(absence);
        }
       
        if (absencesToCreate.Any())
        {
            await absenceRepo.AddRangeAsync(absencesToCreate, ct);
        }
        await SendAsync(new CreateAbsenceResponse($"Absence registered successfully for {absencesToCreate.Count} day(s)."), cancellation: ct);
    }
}

public record CreateAbsenceRequest(DateTime StartDate, DateTime EndDate, int AbsenceStatusId);

public record CreateAbsenceResponse(string Message);

