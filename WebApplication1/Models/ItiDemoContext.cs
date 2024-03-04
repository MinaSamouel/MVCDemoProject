using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class ItiDemoContext : DbContext
{
    public ItiDemoContext()
    {
    }

    public ItiDemoContext(DbContextOptions options)
        : base(options)
    { }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentLearning> StudentLearnings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ItiDemo;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(150)
                .HasColumnName("Course_Name");
            entity.Property(e => e.TotalHours).HasColumnName("Total_Hours");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("Department_Id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .HasColumnName("Department_Name");

            entity.HasMany(d => d.Courses).WithMany(p => p.Departs)
                .UsingEntity<Dictionary<string, object>>(
                    "DeptLearning",
                    r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                    l => l.HasOne<Department>().WithMany().HasForeignKey("DepartId"),
                    j =>
                    {
                        j.HasKey("DepartId", "CourseId");
                        j.ToTable("DeptLearnings");
                        j.HasIndex(new[] { "CourseId" }, "IX_DeptLearnings_CourseId");
                        j.IndexerProperty<int>("DepartId").HasColumnName("DepartID");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.DepartmentId, "IX_Students_DepartmentId");

            entity.Property(e => e.StudentId).HasColumnName("Student_Id");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.StudentAge).HasColumnName("Student_Age");
            entity.Property(e => e.StudentName)
                .HasMaxLength(150)
                .HasColumnName("Student_Name");

            entity.HasOne(d => d.Department).WithMany(p => p.Students)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StudentLearning>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId });

            entity.HasIndex(e => e.CourseId, "IX_StudentLearnings_CourseId");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentLearnings).HasForeignKey(d => d.CourseId);

            entity.HasOne(d => d.Student).WithMany(p => p.StudentLearnings).HasForeignKey(d => d.StudentId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "IX_User_Email").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("User_Name");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRole");
                        j.HasIndex(new[] { "RoleId" }, "IX_UserRole_RoleId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
