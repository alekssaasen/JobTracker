using JobTracker.API.Dtos;
using JobTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.API.Data;

public class JobTrackerContext : DbContext
{
    public DbSet<JobApplication> JobApplications { get; set; }
    
    public JobTrackerContext(DbContextOptions<JobTrackerContext> options) : base(options)
    {
       
    }
    
}