using Boompa.Entities;
using Boompa.Entities.Identity;
//using Boompa.Enums;
using Microsoft.EntityFrameworkCore;

namespace Boompa.Context
{
    public class BoompaContext:DbContext
    {
      
        public BoompaContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Learner> Learners { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<CategoryLearner> CategoryLearners { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SourceMaterial> SourceMaterials { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<ContestRecord> ContestRecords { get; set; }
        public DbSet<CategoryChallenger> CategoryChallengers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("77f04eeb-e4e7-489a-a219-022b7b882b2a"),
                    RoleName = "User",
                    Description = "Apparently dedicated only for Authentication and authorization in the app",
                    CreatedOn = DateTime.MinValue,
                },
                new Role
                {
                    Id = Guid.Parse("12e99949-1ffd-4892-95ca-697f9ebacaae"),
                    RoleName = "Admin",
                    Description = "the user with authority to do certain stuff on user profiles",
                    CreatedOn = DateTime.MinValue
                },
                new Role
                {
                    Id = Guid.Parse("09650940-a428-4bf0-a7aa-352dc1ae2eec"),
                    RoleName = "Learner",
                    Description = "the reason this app is being developed",
                    CreatedOn = DateTime.MinValue
                }
            );

            
           

            

            

            modelBuilder.Entity<SourceMaterial>()
                .HasMany(sm => sm.Questions)
                .WithOne(q => q.SourceMaterial)
                .HasForeignKey(q => q.SourceMaterialId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.SourceMaterials)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            



        }
    }
}
