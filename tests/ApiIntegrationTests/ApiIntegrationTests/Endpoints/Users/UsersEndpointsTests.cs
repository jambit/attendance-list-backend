using System.Net;
using System.Net.Http.Json;
using ALB.Api.UseCases.Endpoints.Users;
using ALB.Api.UseCases.Endpoints.Users.Roles;
using ALB.Domain.Values;
using Bogus;
using Xunit;

namespace ApiIntegrationTests.Endpoints.Users;

public class UsersEndpointsTests(BaseIntegrationTest baseIntegrationTest, ITestOutputHelper output)
    : IClassFixture<BaseIntegrationTest>
{
    private readonly Faker<CreateUserRequest> _userRequestFaker = new Faker<CreateUserRequest>()
        .CustomInstantiator(f => new CreateUserRequest(
            f.Internet.Email(),
            "SoSuperSecureP4a55w0rd!",
            f.Name.FirstName(),
            f.Name.LastName()
        ));

    private HttpClient ParentClient => baseIntegrationTest.GetParentClient();
    private HttpClient AdminClient => baseIntegrationTest.GetAdminClient();

    [Fact]
    public async Task Should_Create_User_Successfully()
    {
        var createUserRequest = _userRequestFaker.Generate();

        var response =
            await AdminClient.PostAsJsonAsync("api/users", createUserRequest, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_User_Already_Exists()
    {
        var createUserRequest = _userRequestFaker.Generate();

        await AdminClient.PostAsJsonAsync("api/users", createUserRequest, TestContext.Current.CancellationToken);

        var response =
            await AdminClient.PostAsJsonAsync("api/users", createUserRequest, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Forbidden_When_Non_Admin_Is_Creating()
    {
        var createUserRequest = _userRequestFaker.Generate();

        var response =
            await ParentClient.PostAsJsonAsync("api/users", createUserRequest, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Should_Get_User_Successfully()
    {
        var createUserRequest = _userRequestFaker.Generate();
        var response =
            await AdminClient.PostAsJsonAsync("api/users", createUserRequest, TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>(TestContext.Current.CancellationToken);
        Assert.NotNull(createdUser);
        var userId = createdUser.Id;

        response = await AdminClient.GetAsync(
            $"api/users/{userId}",
            TestContext.Current.CancellationToken
        );
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Update_User_Successfully()
    {
        var createUserRequest = _userRequestFaker.Generate();
        var response =
            await AdminClient.PostAsJsonAsync("api/users", createUserRequest, TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>(TestContext.Current.CancellationToken);

        Assert.NotNull(createdUser);
        var userId = createdUser.Id;
        var userEmail = "test@userEmail.com";
        var userFirstName = "Max";
        var userLastName = "Mustermann";

        var updateUserRequest = new UpdateUserRequest(userId, userEmail, userFirstName, userLastName);

        response = await AdminClient.PutAsJsonAsync($"api/users/{userId}", updateUserRequest,
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        response = await AdminClient.GetAsync(
            $"api/users/{userId}",
            TestContext.Current.CancellationToken
        );

        var updatedUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>(TestContext.Current.CancellationToken);
        Assert.NotNull(updatedUser);
        Assert.Equal(userEmail, updatedUser.Email);
        Assert.Equal(userFirstName, updatedUser.FirstName);
        Assert.Equal(userLastName, updatedUser.LastName);
    }

    [Fact]
    public async Task Should_Delete_User_Successfully()
    {
        var createUserRequest = _userRequestFaker.Generate();
        var response =
            await AdminClient.PostAsJsonAsync("api/users", createUserRequest, TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>(TestContext.Current.CancellationToken);
        Assert.NotNull(createdUser);
        var userId = createdUser.Id;

        var deleteResponse =
            await AdminClient.DeleteAsync($"api/users/{userId}", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

        var getResponse = await AdminClient.GetAsync(
            $"api/users/{userId}",
            TestContext.Current.CancellationToken
        );
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task Should_Assign_Correct_Role_To_User()
    {
        var createUserRequest = _userRequestFaker.Generate();
        var response =
            await AdminClient.PostAsJsonAsync("api/users", createUserRequest, TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>(TestContext.Current.CancellationToken);
        if (createdUser == null) Assert.Fail("Expected a CreateUserResponse but got null.");
        var userId = createdUser.Id;

        var setRoleRequest = new AddUserRoleRequest
        (
            SystemRoles.Parent
        );

        response = await AdminClient.PostAsJsonAsync($"api/users/{userId}/roles", setRoleRequest,
            TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Forbidden_When_Non_Admin_Is_Assigning_Role()
    {
        var createUserRequest = _userRequestFaker.Generate();
        var response =
            await AdminClient.PostAsJsonAsync("api/users", createUserRequest, TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>(TestContext.Current.CancellationToken);
        if (createdUser == null) Assert.Fail("Expected a CreateUserResponse but got null.");
        var userId = createdUser.Id;

        var setRoleRequest = new AddUserRoleRequest
        (
            SystemRoles.Parent
        );

        response = await ParentClient.PostAsJsonAsync($"api/users/{userId}/roles", setRoleRequest,
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
}