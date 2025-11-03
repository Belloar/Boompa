using Boompa.Entities;
using Boompa.Entities.Identity;
//using Boompa.Enums;
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
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CloudEvalFileDetails> CloudEvalFiles { get; set; }
        public DbSet<CloudSourceFileDetails> CloudSourceFileDetails { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SourceMaterial> SourceMaterials { get; set; }
        public DbSet<SourceFileDetail> SourceFileDetails{ get; set; }
        public DbSet<QuestionFileDetail> QuestionFileDetails{ get; set; }
        public DbSet<Visit> Visits { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleName = "User",
                    Description = "the base entity in the app",
                    CreatedOn = DateTime.MinValue,
                },
                new Role
                {
                    Id = 2,
                    RoleName = "Admin",
                    Description = "the user with authority to do certain stuff on user profiles",
                    CreatedOn = DateTime.MinValue
                },
                new Role
                {
                    Id = 3,
                    RoleName = "Learner",
                    Description = "the reason this app is being developed",
                    CreatedOn = DateTime.MinValue
                });


            
        }
    }
}
