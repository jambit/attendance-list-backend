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
    
    public DbSet<AbsenceDay> AbsenceDays { get; set; }
    public DbSet<AttendanceList> AttendanceLists { get; set; }
    public DbSet<AttendanceListEntry> Attendances { get; set; }
    public DbSet<Child> Children { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Level> Levels { get; set; }
    public DbSet<UserChildRelationship> UserChildRelationships { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<AbsenceDay>(e =>
        {
            e.HasKey(ad => ad.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
    
            e.HasOne(ad => ad.Child)
                .WithMany(c => c.AbsenceDays)
                .HasForeignKey(ad => ad.ChildId);
    
            e.Property(ad => ad.Date)
                .HasColumnType("date");
        });
        
        modelBuilder.Entity<AttendanceList>(e =>
        {
            e.HasKey(al => al.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasMany(al => al.Writers)
                .WithOne(u => u.AttendanceList)
                .HasForeignKey(u => u.AttendanceListId);
    
            e.HasOne(al => al.Grade)
                .WithMany(g => g.AttendanceLists)
                .HasForeignKey(al => al.GradeId);
        });
        
        modelBuilder.Entity<AttendanceListEntry>(e =>
        {
            e.HasKey(ale => ale.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
    
            e.HasOne(ale => ale.Child)
                .WithMany(c => c.AttendanceListEntries)
                .HasForeignKey(ale => ale.ChildId);
    
            e.HasOne(ale => ale.AttendanceList)
                .WithMany(al => al.AttendanceListEntries)
                .HasForeignKey(ale => ale.AttendanceListId);
    
            //Todo AttendanceStatus
            
            e.Property(ale => ale.Date).HasColumnType("date");
            e.Property(ale => ale.ArrivalAt).HasColumnType("time");
            e.Property(ale => ale.DepartureAt).HasColumnType("time");
        });
        
        modelBuilder.Entity<Child>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasOne(c => c.Group)
                .WithMany(g => g.Children)
                .HasForeignKey(c => c.GroupId);
            
            e.HasMany(c => c.AttendanceListEntries)
                .WithOne(ale => ale.Child)
                .HasForeignKey(ale => ale.ChildId);
            
            e.HasMany(c => c.UserChildRelationships)
                .WithOne(ucr => ucr.Child)
                .HasForeignKey(ucr => ucr.ChildId);
            
            e.HasMany(c => c.AbsenceDays)
                .WithOne(ad => ad.Child)
                .HasForeignKey(ad => ad.ChildId);
            
            e.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);
    
            e.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);
    
            e.Property(c => c.DateOfBirth)
                .HasColumnType("date");
        });
        
        modelBuilder.Entity<Grade>(e =>
        {
            e.HasKey(g => g.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasOne(g => g.Group)
                .WithMany(gr => gr.Grades)  
                .HasForeignKey(g => g.GroupId);
            
            e.HasOne(g => g.Level)
                .WithMany(l => l.Grades)    
                .HasForeignKey(g => g.LevelId);
            
            e.HasMany(g => g.AttendanceLists)
                .WithOne(al => al.Grade)
                .HasForeignKey(al => al.GradeId);
            
            e.Property(g => g.CreationYear)
                .IsRequired();
        });

        modelBuilder.Entity<Group>(e =>
        {
            e.HasKey(g => g.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasOne(g => g.ResponsibleUser)
                .WithMany(u => u.ResponsibleGroups)
                .HasForeignKey("ResponsibleUserId");
            
            e.HasMany(g => g.Children)
                .WithOne(c => c.Group)
                .HasForeignKey(c => c.GroupId);
            
            e.HasMany(g => g.UserGroups)
                .WithOne(ug => ug.Group)
                .HasForeignKey(ug => ug.GroupId);
            
            e.HasMany(g => g.Supervisors)
                .WithMany(u => u.SupervisedGroups)  
                .UsingEntity("GroupSupervisors");
            
            e.HasMany(g => g.Grades)
                .WithOne(gr => gr.Group)
                .HasForeignKey(gr => gr.GroupId);
            
            e.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);
        });
        
        modelBuilder.Entity<Level>(e =>
        {
            e.HasKey(l => l.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasMany(l => l.Grades)
                .WithOne(g => g.Level)
                .HasForeignKey(g => g.LevelId);
            
            e.Property(l => l.Description)
                .IsRequired()
                .HasMaxLength(200);
        });
        
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