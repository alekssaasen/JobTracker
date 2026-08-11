using JobTracker.API.Dtos;
using JobTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.API.Data;

public class JobTrackerContext : DbContext
{
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WorkExperience> WorkExperiences { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Reference> References { get; set; }
    public JobTrackerContext(DbContextOptions<JobTrackerContext> options) : base(options)
    {
       
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().ToTable("Companies");
    }
}