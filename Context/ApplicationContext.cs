using Boompa.Entities;
using Boompa.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Boompa.Context
{
    public class ApplicationContext:DbContext
    {
      
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Learner> Learners { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<SourceMaterial> SourceMaterials { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected internal virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }







    }
}
