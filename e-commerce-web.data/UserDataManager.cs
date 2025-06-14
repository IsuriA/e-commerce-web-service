using e_commerce_web.model.Models;
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

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(string username, string password) {

            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Username == username && x.Password == password);

            return user;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
};