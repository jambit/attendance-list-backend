using System.Net;
using System.Net.Http.Json;
using ALB.Api.UseCases.ExampleFeatures.Endpoints.Create;
using Microsoft.AspNetCore.Identity.Data;

namespace ApiIntegrationTests.Users;

[ClassDataSource<BaseIntegrationTest>(Shared = SharedType.PerAssembly)]
public class UsersEndpointsTests(BaseIntegrationTest baseIntegrationTest)
{
    [Test]
    public async Task Should_Return_NotFound_When_User_Is_Not_Found_On_Role_Assignment()
    {
        // Login with admin
        var client = baseIntegrationTest.GetAdminClient();
        var response = await client.PostAsJsonAsync("/login", new LoginRequest
        {
            Email = BaseIntegrationTest.AdminEmail,
            Password = BaseIntegrationTest.AdminPassword
        });
        
        //var response = await client.PostAsJsonAsync("/examples", new CreateExampleRequest("test"));
        
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        // Create new User
        // => post /users
        // Set his Role
        // => post /users/roles
        
        // TODO: finish Test => take token and stuff to reach endpoint.
    }
}