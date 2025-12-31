using e_commerce_web.core.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web.data
{
    public class UserDataManager
    {
        ECommerceDbContext _context;

        public UserDataManager(ECommerceDbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int? roleId = null)
        {
            return await _context.Users
                .Where(u => !roleId.HasValue || _context.UserRoles.First(ur => ur.UserId == u.Id).RoleId == roleId.Value)
                .OrderBy(u => u.FirstName)
                .Select(u => u)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User> GetUserAsync(string username, string password)
        {

            var user = await _context.Users
                .Include(u => u.UserRoleUsers)
                .SingleOrDefaultAsync(x => x.Username == username && x.Password == password);

            return user;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.UserRoleUsers)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public IEnumerable<Role> GetUserRoles(int userId)
        {
            return _context.UserRoles.Where(u => u.UserId == userId)
                .Include(u => u.Role)
                .Select(u => u.Role)
                .OrderBy(r => r.AccessLevel)
                .ToList();
        }

        public async Task<User> AddUserAsync(User user, int roleId)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            await _context.UserRoles.AddAsync(new UserRole { UserId = user.Id, RoleId = roleId });
            await _context.SaveChangesAsync();

            return user;
        }
    }
}