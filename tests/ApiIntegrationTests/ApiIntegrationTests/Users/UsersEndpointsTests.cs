using System.Net;
using System.Net.Http.Json;
using ALB.Api.UseCases.Users.Endpoints.Create;

namespace ApiIntegrationTests.Users;

[ClassDataSource<BaseIntegrationTest>(Shared = SharedType.PerAssembly)]
public class UsersEndpointsTests(BaseIntegrationTest baseIntegrationTest)
{
    
    private HttpClient ParentClient => baseIntegrationTest.GetParentClient();
    private HttpClient AdminClient => baseIntegrationTest.GetAdminClient();

    private readonly CreateUserRequest _createUserRequest = new CreateUserRequest{
        Email = "user@test.com",
        Password = "SuperSecurePassword123!",
        FirstName = "Max",
        LastName = "Mustermann",
    };

    [Test]
    public async Task Should_Create_User_Successfully()
    {
        var response = await AdminClient.PostAsJsonAsync("api/users", _createUserRequest);
        
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Test]
    public async Task Should_Return_Unauthorized_When_Non_Admin_Is_Creating()
    {
        var response = await ParentClient.PostAsJsonAsync("api/users", _createUserRequest);
        
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.Forbidden);
    }
}