namespace AttendanceSystem;

using AttendanceSystem.Entities;
using AttendanceSystem.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


public class TrainningDbContext : DbContext
{
    private readonly string _connectionString;

    private readonly string _assemblyName;

    public TrainningDbContext()
    {
        _connectionString = @"Server=LAPTOP-88GU3220\SQLEXPRESS08;Database=mydb;Trusted_Connection=True";
        _assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_assemblyName));
        }            

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeacherEnrollment>().HasKey(cs => new { cs.CourseId, cs.TeacherId });

        modelBuilder.Entity<StudentEnrollment>().HasKey(cs => new { cs.CourseId, cs.StudentId });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Student> Students { get; set; }

    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Admin> Admins { get; set; }
}
