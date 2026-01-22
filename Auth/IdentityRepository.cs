using Microsoft.EntityFrameworkCore;
using Boompa.Context;
using Boompa.DTO;
using Boompa.Entities.Identity;
using Boompa.Exceptions;

namespace Boompa.Auth
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly BoompaContext _context;
        public IdentityRepository(BoompaContext context)
        {
            _context = context;
        }

        public Task<Role> AddRoleAsync(string role)
        {
            throw new NotImplementedException();
        }
        public async Task AddUserRoles(ICollection<UserRole> userRole)
        {
            var result = 0;
            foreach(var role in userRole)
            {
                _context.UserRoles.Add(role);
                
            }
            
            
        }

        public async Task<bool> CheckUser(string email)
        {
            var result = await _context.Users.AnyAsync(x => x.Email == email && x.IsDeleted == false);
            return result;
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public Task DeleteAsync(Guid id )
        {
            throw new NotImplementedException();
        }
        public async Task<Role> GetRoleAsync(string role)
        {
           var result = await _context.Roles.SingleOrDefaultAsync(r => r.RoleName.ToLower() == role.ToLower());
            if (result == null || result.IsDeleted == true) return null;
            return result;
            
        }
        //public async Task<IEnumerable<string>> GetUserRolesAsync(IEnumerable<int> roleId,int userId)
        //{
        //    var userRoles = _context.UserRoles.Where(x => x.UserId == userId).ToList();
            

        //}
        public async Task<User> GetUserAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<User> GetUserAsync(string searchString,bool isEmail)
        {
            var user =  await _context.Users
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == searchString.ToLower() || u.Email.ToLower() == searchString.ToLower());
            return user;
        }
       
        public async Task<IEnumerable<User>> GetUsersAsync(int pageNumber)
        {
            var result = await _context.Users.Include(u => u.Roles).ThenInclude(ur => ur.Role).Skip(pageNumber).Take(pageNumber+50).ToListAsync();
                
            return result;
        }
        public async Task UpdateAsync(Guid id, User updatedUser)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            //if (user == null) return 0;
            
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            _context.Users.Update(user);
            //var result = await _context.SaveChangesAsync();
            //return result;

        }

        public async Task UpdateUserRole(Guid id,Role role)
        {
                
            var newRole = new UserRole
            {
                UserId = id,
                RoleId = role.Id,
            };
            await _context.UserRoles.AddAsync(newRole);
            //return await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<string>> GetRolesAsync()
        {
            throw new NotImplementedException();
        }

       
    }
}
