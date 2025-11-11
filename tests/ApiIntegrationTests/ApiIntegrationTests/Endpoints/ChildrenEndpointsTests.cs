using System.Net;
using System.Net.Http.Json;

using ALB.Api.Endpoints.Children;

using NodaTime;

namespace ApiIntegrationTests.Endpoints;

[ClassDataSource<BaseIntegrationTest>(Shared = SharedType.PerAssembly)]
public class ChildrenEndpointsTests(BaseIntegrationTest baseIntegrationTest)
{

    [Test]
    public async Task Should_Create_Child_Successfully()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var createChildRequest = TestDataFaker.ChildRequestFaker.Generate();

        var response =
            await adminClient.PostAsJsonAsync("api/children", createChildRequest);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task Should_Get_Child_Successfully()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var createChildRequest = TestDataFaker.ChildRequestFaker.Generate();
        var response =
            await adminClient.PostAsJsonAsync("api/children", createChildRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var createdChild =
            await response.Content.ReadFromJsonAsync<CreateChildResponse>();
        await Assert.That(createdChild).IsNotNull();
        var childId = createdChild!.Id;

        response = await adminClient.GetAsync(
            $"api/children/{childId}"
        );
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task Should_Delete_Child_Successfully()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var createChildRequest = TestDataFaker.ChildRequestFaker.Generate();
        var response =
            await adminClient.PostAsJsonAsync("api/children", createChildRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var createdChild =
            await response.Content.ReadFromJsonAsync<CreateChildResponse>();
        await Assert.That(createdChild).IsNotNull();
        var childId = createdChild!.Id;

        var deleteResponse =
            await adminClient.DeleteAsync($"api/children/{childId}");
        await Assert.That(deleteResponse.StatusCode).IsEqualTo(HttpStatusCode.NoContent);

        var getResponse = await adminClient.GetAsync(
            $"api/children/{childId}"
        );
        await Assert.That(getResponse.StatusCode).IsEqualTo(HttpStatusCode.NotFound);
        deleteResponse =
            await adminClient.DeleteAsync($"api/children/{childId}");
        await Assert.That(deleteResponse.StatusCode).IsEqualTo(HttpStatusCode.NotFound);
    }

    [Test]
    [Skip("Bug with deserialization of LocalDate")]
    public async Task Should_Update_Child_Successfully()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var createChildRequest = TestDataFaker.ChildRequestFaker.Generate();

        var response =
            await adminClient.PostAsJsonAsync("api/children", createChildRequest);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var createdChild =
            await response.Content.ReadFromJsonAsync<CreateChildResponse>();

        await Assert.That(createdChild).IsNotNull();

        var childId = createdChild!.Id;
        var childFirstName = "Max";
        var childLastName = "Mustermann";
        var childDateOfBirth = new LocalDate(2023, 8, 11);

        var updateChildRequest = new UpdateChildRequest(childFirstName, childLastName, childDateOfBirth);

        response = await adminClient.PutAsJsonAsync($"api/children/{childId}", updateChildRequest);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.NoContent);

        response = await adminClient.GetAsync(
            $"api/children/{childId}"
        );

        var updatedChild =
            await response.Content.ReadFromJsonAsync<GetChildResponse>();

        await Assert.That(updatedChild).IsNotNull();
        await Assert.That(updatedChild!.FirstName).IsEqualTo(childFirstName);
        await Assert.That(updatedChild.LastName).IsEqualTo(childLastName);
        await Assert.That(updatedChild.DateOfBirth).IsEqualTo(childDateOfBirth);
    }
}