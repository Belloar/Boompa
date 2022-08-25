using Boompa.Entities;
using Boompa.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Boompa.Context
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Learner> Learners { get; set; }
        public DbSet<Diary> Diaries { get; set; }

    }
}
