using ALB.Domain.Entities;
using ALB.Domain.Repositories;
using FastEndpoints;

namespace ALB.Api.UseCases.Endpoints.Children.Absence;

public class CreateAbsenceForChildEndpoint(IAbsenceDayRepository absenceRepo) : Endpoint<CreateAbsenceRequest, CreateAbsenceResponse>
{
    public override void Configure()
    {
        Post("/api/children/{childId:guid}/absence");
        Policies("ParentPolicy");
    }

    public override async Task HandleAsync(CreateAbsenceRequest request, CancellationToken ct)
    {
        var childId = Route<Guid>("ChildId");

        var alreadyExists = await absenceRepo.ExistsAsync(childId, DateOnly.FromDateTime(request.Date));
        
        if (alreadyExists)
        {
            await SendAsync(new CreateAbsenceResponse(null, "Absence already exists for this date."), 400, ct);
            return;
        }

        var absence = new AbsenceDay
        {
            Id = Guid.NewGuid(),
            ChildId = childId,
            Date = DateOnly.FromDateTime(request.Date),
            AbsenceStatusId = request.AbsenceStatusId,
        };

        await absenceRepo.AddAsync(absence);

        await SendAsync(new CreateAbsenceResponse(absence.Id, "Absence registered successfully."), cancellation: ct);
    }
}




public record CreateAbsenceRequest(DateTime Date, int AbsenceStatusId);

public record CreateAbsenceResponse(Guid? AbsenceId, string Message);


