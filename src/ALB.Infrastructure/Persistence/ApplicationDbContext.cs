﻿using ALB.Domain.Entities;
using ALB.Domain.Enum;
using ALB.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NodaTime;

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
    public DbSet<AttendanceListWriter> AttendanceListWriters { get; set; }
    public DbSet<Child> Children { get; set; }
    public DbSet<Cohort> Cohorts { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Grade> Grades{ get; set; }
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
            
            e.HasOne(ad => ad.AbsenceStatus)
                .WithMany(c => c.AbsenceDays)
                .HasForeignKey(ad => ad.AbsenceStatusId);
    
            e.Property(ad => ad.Date)
                .HasColumnType("date");
        });
        
        modelBuilder.Entity<AbsenceStatus>(e =>
        {
            e.HasKey(a => a.Id);
            
            e.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);
            
            e.HasData(
                new AbsenceStatus { Id = 1, Name = "Sick" },
                new AbsenceStatus { Id = 2, Name = "Holiday" }
            );
        });
        
        modelBuilder.Entity<AttendanceList>(e =>
        {
            e.HasKey(al => al.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasMany(al => al.Writers)
                .WithOne(alw => alw.AttendanceList)
                .HasForeignKey(alw => alw.AttendanceListId);
    
            e.HasOne(al => al.Cohort)
                .WithMany(g => g.AttendanceLists)
                .HasForeignKey(al => al.CohortId);
                
            e.Property(al => al.ValidationPeriod)
                .HasConversion(
                    v => new { Start = v.Start, End = v.End },
                    v => new DateInterval(v.Start, v.End))
                .HasColumnType("jsonb");
        });
        
        modelBuilder.Entity<AttendanceListWriter>(e =>
        {
            e.HasKey(alw => new { alw.UserId, alw.AttendanceListId });
            
            e.HasOne(alw => alw.User)
                .WithMany(u => u.WriterAssignments)
                .HasForeignKey(alw => alw.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            e.HasOne(alw => alw.AttendanceList)
                .WithMany(al => al.Writers)
                .HasForeignKey(alw => alw.AttendanceListId)
                .OnDelete(DeleteBehavior.Restrict);
            
            e.Property(alw => alw.AssignedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("now()");
            
            e.HasIndex(alw => alw.AttendanceListId);
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
    
            e.HasOne(ale => ale.AttendanceStatus)
                .WithMany(c => c.AttendanceListEntries)
                .HasForeignKey(ale => ale.AttendanceStatusId);
            
            e.Property(ale => ale.Date).HasColumnType("date");
            e.Property(ale => ale.ArrivalAt).HasColumnType("time");
            e.Property(ale => ale.DepartureAt).HasColumnType("time");
        });

        modelBuilder.Entity<AttendanceStatus>(e =>
        {
            e.HasKey(a => a.Id);
            
            e.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);

            e.HasData(
                new AttendanceStatus { Id = 1, Name = "Present" },
                new AttendanceStatus { Id = 2, Name = "Excused" },
                new AttendanceStatus { Id = 3, Name = "Late" }
            );
            
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
        
        modelBuilder.Entity<Cohort>(e =>
        {
            e.HasKey(g => g.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasOne(g => g.Group)
                .WithMany(gr => gr.Cohorts)  
                .HasForeignKey(g => g.GroupId);

            e.HasOne(x => x.Grade)
                .WithOne(x => x.Cohort)
                .HasForeignKey<Cohort>(x => x.GradeId);
            
            e.HasMany(g => g.AttendanceLists)
                .WithOne(al => al.Cohort)
                .HasForeignKey(al => al.CohortId);
            
            e.Property(g => g.CreationYear)
                .IsRequired();
        });

        modelBuilder.Entity<Group>(e =>
        {
            e.HasKey(g => g.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.HasOne(g => g.ResponsibleUser)
                .WithMany(u => u.ResponsibleGroups)
                .HasForeignKey(g => g.ResponsibleUserId);
            
            e.HasMany(g => g.Children)
                .WithOne(c => c.Group)
                .HasForeignKey(c => c.GroupId);
            
            e.HasMany(g => g.UserGroups)
                .WithOne(ug => ug.Group)
                .HasForeignKey(ug => ug.GroupId);
            
            e.HasMany(g => g.Cohorts)
                .WithOne(gr => gr.Group)
                .HasForeignKey(gr => gr.GroupId);
            
            e.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);
        });
        
        modelBuilder.Entity<Grade>(e =>
        {
            e.HasKey(l => l.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd().HasValueGenerator<UuiDv7Generator>();
            
            e.Property(l => l.Description)
                .IsRequired()
                .HasMaxLength(200);
        });
        
        modelBuilder.Entity<UserGroup>(e =>
        {
            e.HasKey(ug => new { ug.UserId, ug.GroupId });
            
            e.HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
    
            e.HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
            
            e.Property(ug => ug.IsSupervisor)
                .HasDefaultValue(false);
            
            e.HasIndex(ug => ug.GroupId);
        });

        modelBuilder.Entity<UserChildRelationship>(e =>
        {
            e.HasKey(ucr => new { ucr.UserId, ucr.ChildId });
            
            e.HasOne(ucr => ucr.User)
                .WithMany(u => u.UserChildRelationships)
                .HasForeignKey(ucr => ucr.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        
            e.HasOne(ucr => ucr.Child)
                .WithMany(c => c.UserChildRelationships)
                .HasForeignKey(ucr => ucr.ChildId)
                .OnDelete(DeleteBehavior.Restrict);
            
            e.HasIndex(ucr => ucr.ChildId);
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
            
            b.HasMany(u => u.WriterAssignments)
                .WithOne(alw => alw.User)
                .HasForeignKey(alw => alw.UserId);
            
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
            
            b.Property(p => p.Description).HasMaxLength(200).IsRequired();
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