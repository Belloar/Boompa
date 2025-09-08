using Microsoft.EntityFrameworkCore;
using Boompa.Context;
using Boompa.DTO;
using Boompa.Entities.Identity;

namespace Boompa.Auth
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly ApplicationContext _context;
        public IdentityRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Task<Role> AddRoleAsync(string role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task<int> AddUserRole(IEnumerable<UserRole> userRole, CancellationToken cancellationToken)
        {
            var result = 0;
            foreach(var role in userRole)
            {
                _context.UserRoles.Add(role);
                result = await _context.SaveChangesAsync(cancellationToken);
                if (result == 0) return 0;
            }
            
            return result;
        }

        public async Task<bool> CheckUser(string email)
        {
            var result = await _context.Users.AnyAsync(x => x.Email == email && x.IsDeleted == false);
            return result;
        }

        public async Task<int> CreateAsync(User user, CancellationToken cancellationToken)
        {
            await AddUserRole(user.Roles, cancellationToken);
            await _context.Users.AddAsync(user, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
        public Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
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
            var userRoles = _context.UserRoles.Include(r => r.Role).Where(x => x.UserId == user.Id).Select(r => r) ;
            foreach (var userRole in userRoles) user.Roles.Add(userRole);

            return user;
        }
       
        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateAsync(int id, User updatedUser, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) return 0;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            _context.Users.Update(user);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;

        }

        public async Task<int> UpdateUserRole(int id,Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var newRole = new UserRole
            {
                UserId = id,
                RoleId = role.Id,
            };
            await _context.UserRoles.AddAsync(newRole);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<IEnumerable<string>> GetRolesAsync()
        {
            throw new NotImplementedException();
        }

       
    }
}
