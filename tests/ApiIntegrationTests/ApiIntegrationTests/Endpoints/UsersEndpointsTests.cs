using System.Net;
using System.Net.Http.Json;

using ALB.Api.Endpoints.Users;
using ALB.Api.Endpoints.Users.Roles;
using ALB.Domain.Values;

namespace ApiIntegrationTests.Endpoints;

[ClassDataSource<BaseIntegrationTest>(Shared = SharedType.PerAssembly)]
public class UsersEndpointsTests(BaseIntegrationTest baseIntegrationTest)
{

    [Test]
    public async Task Should_Create_User_Successfully()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var createUserRequest = TestDataFaker.UserRequestFaker.Generate();

        var response =
            await adminClient.PostAsJsonAsync("api/users", createUserRequest);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task Should_Return_BadRequest_When_User_Already_Exists()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var createUserRequest = TestDataFaker.UserRequestFaker.Generate();

        await adminClient.PostAsJsonAsync("api/users", createUserRequest);

        var response =
            await adminClient.PostAsJsonAsync("api/users", createUserRequest);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task Should_Return_Forbidden_When_Non_Admin_Is_Creating()
    {
        var parentClient = baseIntegrationTest.GetParentClient();
        var createUserRequest = TestDataFaker.UserRequestFaker.Generate();

        var response =
            await parentClient.PostAsJsonAsync("api/users", createUserRequest);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Forbidden);
    }

    [Test]
    public async Task Should_Get_User_Successfully()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var createUserRequest = TestDataFaker.UserRequestFaker.Generate();
        var response =
            await adminClient.PostAsJsonAsync("api/users", createUserRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var createdUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>();
        await Assert.That(createdUser).IsNotNull();
        var userId = createdUser!.Id;

        response = await adminClient.GetAsync(
            $"api/users/{userId}"
        );
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task Should_Update_User_Successfully()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var createUserRequest = TestDataFaker.UserRequestFaker.Generate();
        var response =
            await adminClient.PostAsJsonAsync("api/users", createUserRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var createdUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>();

        await Assert.That(createdUser).IsNotNull();

        var userId = createdUser!.Id;
        var userFirstName = "Max";
        var userLastName = "Mustermann";

        var updateUserRequest = new UpdateUserRequest(userFirstName, userLastName);

        response = await adminClient.PutAsJsonAsync($"api/users/{userId}", updateUserRequest);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.NoContent);

        response = await adminClient.GetAsync(
            $"api/users/{userId}"
        );

        var getUserResponse =
            await response.Content.ReadFromJsonAsync<GetUserResponse>();
        var updatedUser = getUserResponse?.User;
        await Assert.That(updatedUser).IsNotNull();
        await Assert.That(updatedUser!.FirstName).IsEqualTo(userFirstName);
        await Assert.That(updatedUser.LastName).IsEqualTo(userLastName);
    }

    [Test]
    public async Task Should_Delete_User_Successfully()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var createUserRequest = TestDataFaker.UserRequestFaker.Generate();
        var response =
            await adminClient.PostAsJsonAsync("api/users", createUserRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var createdUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>();
        await Assert.That(createdUser).IsNotNull();
        var userId = createdUser!.Id;

        var deleteResponse =
            await adminClient.DeleteAsync($"api/users/{userId}");
        await Assert.That(deleteResponse.StatusCode).IsEqualTo(HttpStatusCode.NoContent);

        var getResponse = await adminClient.GetAsync(
            $"api/users/{userId}"
        );
        await Assert.That(getResponse.StatusCode).IsEqualTo(HttpStatusCode.NotFound);

        deleteResponse =
            await adminClient.DeleteAsync($"api/users/{userId}");
        await Assert.That(deleteResponse.StatusCode).IsEqualTo(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Should_Assign_Correct_Role_To_User()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var createUserRequest = TestDataFaker.UserRequestFaker.Generate();
        var response =
            await adminClient.PostAsJsonAsync("api/users", createUserRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var createdUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>();
        await Assert.That(createdUser).IsNotNull();
        var userId = createdUser!.Id;

        var setRoleRequest = new AddUserRoleRequest
        (
            SystemRoles.Parent
        );

        response = await adminClient.PostAsJsonAsync($"api/users/{userId}/roles", setRoleRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.NoContent);
    }

    [Test]
    public async Task Should_Return_Forbidden_When_Non_Admin_Is_Assigning_Role()
    {
        var adminClient = baseIntegrationTest.GetAdminClient();
        var parentClient = baseIntegrationTest.GetParentClient();
        var createUserRequest = TestDataFaker.UserRequestFaker.Generate();
        var response =
            await adminClient.PostAsJsonAsync("api/users", createUserRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

        var createdUser =
            await response.Content.ReadFromJsonAsync<CreateUserResponse>();
        await Assert.That(createdUser).IsNotNull();
        var userId = createdUser!.Id;

        var setRoleRequest = new AddUserRoleRequest
        (
            SystemRoles.Parent
        );

        response = await parentClient.PostAsJsonAsync($"api/users/{userId}/roles", setRoleRequest);

        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Forbidden);
    }
}