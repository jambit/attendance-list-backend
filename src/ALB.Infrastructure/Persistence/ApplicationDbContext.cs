using ALB.Domain.Entities;
using ALB.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ALB.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Group> Groups { get; set; }
    public DbSet<Child> Children { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<UserChildRelationship> UserChildRelationships { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure many-to-many relationship between user and group
        modelBuilder.Entity<UserGroup>()
            .HasKey(ug => ug.Id);
            
        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.User)
            .WithMany(u => u.UserGroups)
            .HasForeignKey(ug => ug.UserId);
            
        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.Group)
            .WithMany(g => g.UserGroups)
            .HasForeignKey(ug => ug.GroupId);
            
        // Configure many-to-many relationship between user and child
        modelBuilder.Entity<UserChildRelationship>()
            .HasKey(ucr => ucr.Id);
            
        modelBuilder.Entity<UserChildRelationship>()
            .HasOne(ucr => ucr.User)
            .WithMany(u => u.UserChildRelationships)
            .HasForeignKey(ucr => ucr.UserId);
            
        modelBuilder.Entity<UserChildRelationship>()
            .HasOne(ucr => ucr.Child)
            .WithMany(c => c.UserChildRelationships)
            .HasForeignKey(ucr => ucr.ChildId);
        
        // Configure many-to-many relationship between user and role
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });
    
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);
    
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
            
        // Configure one-to-many relationship between child and attendance
        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Child)
            .WithMany(c => c.Attendances)
            .HasForeignKey(a => a.ChildId);
        
        // Configure one-to-many relationship between group and child
        modelBuilder.Entity<Child>()
            .HasOne(c => c.Group)
            .WithMany(g => g.Children)
            .HasForeignKey(c => c.GroupId);
    }
}