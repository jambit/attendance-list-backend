using ALB.Domain.Entities;
using ALB.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ALB.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Group> Groups { get; set; }
    public DbSet<Child> Children { get; set; }
    public DbSet<AttendanceListEntry> Attendances { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<UserChildRelationship> UserChildRelationships { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserGroup>(e =>
        {
            e.HasKey(ug => ug.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);
            
            e.HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);
        });
        
        modelBuilder.Entity<UserChildRelationship>(e =>
        {
            e.HasKey(ucr => ucr.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            e.HasOne(ucr => ucr.User)
                .WithMany(u => u.UserChildRelationships)
                .HasForeignKey(ucr => ucr.UserId);
            e.HasOne(ucr => ucr.Child)
                .WithMany(c => c.UserChildRelationships)
                .HasForeignKey(ucr => ucr.ChildId);
        });
        
        modelBuilder.Entity<AttendanceListEntry>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasOne(a => a.Child)
                .WithMany(c => c.Attendances)
                .HasForeignKey(a => a.ChildId);
        });
        
        modelBuilder.Entity<Child>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasOne(c => c.Group)
                .WithMany(g => g.Children)
                .HasForeignKey(c => c.GroupId);
        });

        modelBuilder.Entity<Group>(e =>
        {
            e.HasKey(g => g.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasMany(g => g.Children)
                .WithOne(c => c.Group)
                .HasForeignKey(c => c.GroupId);
            
            e.HasMany(g => g.UserGroups)
                .WithOne(ug => ug.Group)
                .HasForeignKey(ug => ug.GroupId);
        });
        
        //Configuration of ApplicationUser
        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();

            b.Property(p => p.FirstName).HasMaxLength(50);
            b.Property(p => p.LastName).HasMaxLength(50);
            
            b.Property(p => p.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("now()");
            // Each User can have many UserClaims
            b.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            b.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            b.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<ApplicationRole>(b =>
        {
            b.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            // Each Role can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            b.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        });
        
        modelBuilder.Entity<ApplicationUserClaim>(b =>
        {
            b.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        });
        
        modelBuilder.Entity<ApplicationRoleClaim>(b =>
        {
            b.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
        });
    }
}