using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByMatriculeAsync(int matricule)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Matricule == matricule);
        }

        public async Task<bool> UserExistsAsync(int matricule)
        {
            return await _context.Users
                .AnyAsync(u => u.Matricule == matricule);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task CreateUser(User user)
        {
            // Check if Matricule is unique
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Matricule == user.Matricule);
            
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this matricule already exists.");
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public Task<List<User?>?> getAllUsers()
        {
            return _context.Users
                            .ToListAsync();
        }
         public async Task UpdateUser(User user)
{
    // Fetch the existing user from the database
    var existingUser = await _context.Users.FindAsync(user.Id);
    if (existingUser == null)
    {
        throw new InvalidOperationException("User not found.");
    }

    // Update the existing user with new values
    _context.Entry(existingUser).CurrentValues.SetValues(user);

    // Save changes
    await _context.SaveChangesAsync();
}


        public async Task DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }



        
    }
}
