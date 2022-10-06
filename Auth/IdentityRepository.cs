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
        public async Task<int> CreateAsync(User user, CancellationToken cancellationToken)
        {
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
           var result = _context.Roles.SingleOrDefault(r => r.RoleName.ToLower() == role.ToLower());
            if (result == null) throw ArgumentNullException("");
            
        }
        public Task<User> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserAsync(string checkString)
        {
            throw new NotImplementedException();
        }
        public User GetUserAsync(string checkString, string password)
        {
            if(checkString.Contains('@'))
            {
                return _context.Users.FirstOrDefault(x => x.Email.ToLower() == checkString.ToLower() && x.Password == password);
            }
            return _context.Users.FirstOrDefault(x => x.UserName.ToLower() == checkString.ToLower() && x.Password == password);
            
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
        

    }
}
