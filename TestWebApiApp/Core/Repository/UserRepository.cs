using Microsoft.EntityFrameworkCore;
using TestWebApiApp.Core.Models;
using TestWebApiApp.Infrastructure;

namespace TestWebApiApp.Core.Repository
{
    public class UserRepository(ApplicationDbContext _context) : IUserRepository
    {
        //private readonly ApplicationDbContext _context;

        //public UserRepository(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<User> GetByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FindAsync(new object[] { userId }, cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        // Other methods as needed...

    }
}
