using System.Net;
using System.Net.Http.Json;
using ALB.Api.UseCases.Users.Endpoints.Create;
using ALB.Api.UseCases.Users.Endpoints.SetRole;
using ALB.Domain.Values;

namespace ApiIntegrationTests.Users;

[ClassDataSource<BaseIntegrationTest>(Shared = SharedType.PerAssembly)]
public class UsersEndpointsTests(BaseIntegrationTest baseIntegrationTest)
{
    
    private HttpClient ParentClient => baseIntegrationTest.GetParentClient();
    private HttpClient AdminClient => baseIntegrationTest.GetAdminClient();
    
    private static int _userCounter = 0;

    private CreateUserRequest CreateUniqueUserRequest()
    {
        var counter = Interlocked.Increment(ref _userCounter);
        
        return new CreateUserRequest{
            Email = $"user{counter}@test.com",
            Password = "SuperSecurePassword123!",
            FirstName = "Max",
            LastName = "Mustermann",
        };
    }
    
    [Test]
    public async Task Should_Create_User_Successfully()
    {

        var createUserRequest = CreateUniqueUserRequest();
        
        var response = await AdminClient.PostAsJsonAsync("api/users", createUserRequest);
        
        var content = await response.Content.ReadAsStringAsync();
        
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task Should_Return_BadRequest_When_User_Already_Exists()
    {
        var createUserRequest = CreateUniqueUserRequest();
        await AdminClient.PostAsJsonAsync("api/users", createUserRequest);
        var response = await AdminClient.PostAsJsonAsync("api/users", createUserRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task Should_Return_Unauthorized_When_Non_Admin_Is_Creating()
    {
        
        var createUserRequest = CreateUniqueUserRequest();
        
        var response = await ParentClient.PostAsJsonAsync("api/users", createUserRequest);
        
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Forbidden);
    }
    
    [Test]
    public async Task Should_Assign_Correct_Role_To_User()
    {
        var createUserRequest = CreateUniqueUserRequest();
        var response = await AdminClient.PostAsJsonAsync("api/users", createUserRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    
        var createdUser = await response.Content.ReadFromJsonAsync<CreateUserResponse>();
        var userId = createdUser.Id;
    
        var setRoleRequest = new SetUserRoleRequest
        {
            UserId = userId,
            RoleValue = SystemRoles.Parent
        };
    
        response = await AdminClient.PostAsJsonAsync("api/users/roles", setRoleRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task Should_Return_Unauthorized_When_Non_Admin_Is_Assigning_Role()
    {
        var createUserRequest = CreateUniqueUserRequest();
        var response = await AdminClient.PostAsJsonAsync("api/users", createUserRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    
        var createdUser = await response.Content.ReadFromJsonAsync<CreateUserResponse>();
        var userId = createdUser.Id;
    
        var setRoleRequest = new SetUserRoleRequest
        {
            UserId = userId,
            RoleValue = SystemRoles.Parent
        };
    
        response = await ParentClient.PostAsJsonAsync("api/users/roles", setRoleRequest);
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Forbidden);
    }
    
}