using Microsoft.EntityFrameworkCore;

namespace Website.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
    }
}