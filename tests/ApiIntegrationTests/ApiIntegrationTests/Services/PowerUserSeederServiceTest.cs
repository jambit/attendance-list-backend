using ALB.Domain.Identity;
using ALB.Domain.Values;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ApiIntegrationTests.Services;

public class PowerUserSeederServiceTest(BaseIntegrationTest baseIntegrationTest) : IClassFixture<BaseIntegrationTest>
{
    [Fact]
    public async Task Should_Create_Power_User_On_Startup()
    {
        using var scope = baseIntegrationTest.GetScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var powerUser = await userManager.FindByEmailAsync("admin@attendance-list-backend.de");
        var isAdmin = powerUser != null && await userManager.IsInRoleAsync(powerUser, SystemRoles.Admin);

        Assert.NotNull(powerUser);
        Assert.True(isAdmin);
    }
}