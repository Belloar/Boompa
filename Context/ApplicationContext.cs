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
        public DbSet<Article> Articles { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<QuestionPhoto> QuestionPhotos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Visit> Visits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleName = "User",
                    Description = "the base entity in the app"
                },
                new Role
                {
                    Id = 2,
                    RoleName = "Admin",
                    Description = "the user with authority to do certain stuff on user profiles"
                },
                new Role
                {
                    Id = 3,
                    RoleName = "Learner",
                    Description = "the reason this app is being developed"
                });


            /*modelBuilder.Entity<User>()
                 .HasData(
                new User
                {
                    Id = 1,
                    UserName = "SupremeRuler001",
                    Password = "0001",
                    Email = "theOwner@gmail.com",
                    PhoneNumber = "05062222",
                    IsEmailConfirmed = true,
                    Hashsalt = Guid.NewGuid().ToString(),

                },
                new User
                {
                    Id = 2,
                    UserName = "User001",
                    Password = "firstUser",
                    Email = "zaFirstUser@gmail.com",
                    PhoneNumber = "05062222",
                    IsEmailConfirmed = true,
                    Hashsalt = Guid.NewGuid().ToString(),

                });
            modelBuilder.Entity<UserRole>().HasData(
                        new UserRole { Id = 1, UserId = 1, RoleId = 1 },
                        new UserRole { Id = 2, UserId = 2, RoleId = 1 }
                    );*/
        }
    }
}
