using System.Net;
using System.Net.Http.Json;
using ALB.Api.Endpoints.Children;
using Bogus;
using NodaTime;
using Xunit;

namespace ApiIntegrationTests.Endpoints;

public class ChildrenEndpointsTests(BaseIntegrationTest baseIntegrationTest, ITestOutputHelper testOutputHelper)
    : IClassFixture<BaseIntegrationTest>
{
    private readonly Faker<CreateChildRequest> _childRequestFaker = new Faker<CreateChildRequest>()
        .CustomInstantiator(f => new CreateChildRequest(
            f.Name.FirstName(),
            f.Name.LastName(),
            LocalDate.FromDateTime(f.Date.Past(10, DateTime.Today))
        ));

    private HttpClient ParentClient => baseIntegrationTest.GetParentClient();
    private HttpClient AdminClient => baseIntegrationTest.GetAdminClient();

    [Fact]
    public async Task Should_Create_Child_Successfully()
    {
        var createChildRequest = _childRequestFaker.Generate();

        var response =
            await AdminClient.PostAsJsonAsync("api/children", createChildRequest,
                TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Get_Child_Successfully()
    {
        var createChildRequest = _childRequestFaker.Generate();
        var response =
            await AdminClient.PostAsJsonAsync("api/children", createChildRequest,
                TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdChild =
            await response.Content.ReadFromJsonAsync<CreateChildResponse>(TestContext.Current.CancellationToken);
        Assert.NotNull(createdChild);
        var childId = createdChild.Id;

        response = await AdminClient.GetAsync(
            $"api/children/{childId}",
            TestContext.Current.CancellationToken
        );
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Delete_Child_Successfully()
    {
        var createChildRequest = _childRequestFaker.Generate();
        var response =
            await AdminClient.PostAsJsonAsync("api/children", createChildRequest,
                TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdChild =
            await response.Content.ReadFromJsonAsync<CreateChildResponse>(TestContext.Current.CancellationToken);
        Assert.NotNull(createdChild);
        var childId = createdChild.Id;

        var deleteResponse =
            await AdminClient.DeleteAsync($"api/children/{childId}", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getResponse = await AdminClient.GetAsync(
            $"api/children/{childId}",
            TestContext.Current.CancellationToken
        );
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        deleteResponse =
            await AdminClient.DeleteAsync($"api/children/{childId}", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task Should_Update_Child_Successfully()
    {
        var createChildRequest = _childRequestFaker.Generate();

        testOutputHelper.WriteLine(createChildRequest.ToString());

        var response =
            await AdminClient.PostAsJsonAsync("api/children", createChildRequest,
                TestContext.Current.CancellationToken);

        testOutputHelper.WriteLine(response.Content.ReadAsStringAsync().Result);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdChild =
            await response.Content.ReadFromJsonAsync<CreateChildResponse>(TestContext.Current.CancellationToken);

        Assert.NotNull(createdChild);

        var childId = createdChild.Id;
        var childFirstName = "Max";
        var childLastName = "Mustermann";
        var childDateOfBirth = new LocalDate(2023, 8, 11);

        var updateChildRequest = new UpdateChildRequest(childFirstName, childLastName, childDateOfBirth);

        response = await AdminClient.PutAsJsonAsync($"api/children/{childId}", updateChildRequest,
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        response = await AdminClient.GetAsync(
            $"api/children/{childId}",
            TestContext.Current.CancellationToken
        );

        var updatedChild =
            await response.Content.ReadFromJsonAsync<GetChildResponse>(TestContext.Current.CancellationToken);

        Assert.NotNull(updatedChild);
        Assert.Equal(childFirstName, updatedChild.FirstName);
        Assert.Equal(childLastName, updatedChild.LastName);
        Assert.Equal(childDateOfBirth, updatedChild.DateOfBirth);
    }
}