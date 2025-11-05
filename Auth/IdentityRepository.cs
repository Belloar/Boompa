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
        public async Task AddUserRole(IEnumerable<UserRole> userRole)
        {
            var result = 0;
            foreach(var role in userRole)
            {
                _context.UserRoles.Add(role);
                //result = await _context.SaveChangesAsync();
                //if (result == 0) return 0;
            }
            
            
        }

        public async Task<bool> CheckUser(string email)
        {
            var result = await _context.Users.AnyAsync(x => x.Email == email && x.IsDeleted == false);
            return result;
        }

        public async Task CreateAsync(User user)
        {
            await AddUserRole(user.Roles);
            await _context.Users.AddAsync(user);
            //var result = await _context.SaveChangesAsync();
            //return result;
        }
        public Task DeleteAsync(int id )
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
        public async Task<User> GetUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<User> GetUserAsync(string searchString,bool isEmail)
        {
            if (isEmail) _ =  _context.Users.SingleOrDefault(u => u.Email.ToLower() == searchString.ToLower());
            var user =   _context.Users.SingleOrDefault(u => u.UserName.ToLower() == searchString.ToLower());
            var userRoles = _context.UserRoles.Include(r => r.Role).Where(x => x.UserId == user.Id).Select(r => r);
            foreach (var userRole in userRoles)
            {
                user.Roles.Add(userRole);
            }

            return user;
        }
       
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                var result = await _context.Users.Select(u => u).ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw new IdentityException(ex.Message);
            }

        }
        public async Task UpdateAsync(int id, User updatedUser)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            //if (user == null) return 0;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            _context.Users.Update(user);
            //var result = await _context.SaveChangesAsync();
            //return result;

        }

        public async Task UpdateUserRole(int id,Role role)
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
