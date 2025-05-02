using ALB.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ALB.Infrastructure.Persistence;

public class AlbDbContext : IdentityDbContext<AlbUser>
{
    public AlbDbContext(DbContextOptions<AlbDbContext> options) : base(options)
    {
        
    }
}