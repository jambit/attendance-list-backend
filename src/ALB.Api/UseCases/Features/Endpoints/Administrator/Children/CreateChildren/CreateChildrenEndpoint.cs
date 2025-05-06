using FastEndpoints; 

namespace ALB.Api.UseCases.Features.Endpoints.Administrator.Children.CreateChildren;

public class CreateChildrenEndpoint : Endpoint<CreateChildrenRequest, CreateChildrenResponse>
{
    public override void Configure()
    {
        Post("/admin/children/create-children");
    }

    public override async Task HandleAsync(CreateChildrenRequest request, CancellationToken cancellationToken)
    {
        var createdChildId = Guid.NewGuid(); //hier muss der Datenbankzugriff hin -> Erstellen eines neuen eintrages

        await SendAsync(new CreateChildrenResponse
        {
            Id = createdChildId,
            Message = $"Child {createdChildId} created successfully",
        });
    }
}