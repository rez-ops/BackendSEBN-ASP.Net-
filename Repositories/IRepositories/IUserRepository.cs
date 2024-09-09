using System;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IUserRepository
{
  Task<User> GetUserByMatriculeAsync(int matricule);
  Task<bool> UserExistsAsync(int matricule);
  Task<User?> GetUserByIdAsync(int userId);
  Task CreateUser(User user);
  Task<List<User?>?> getAllUsers();

  Task UpdateUser(User user);
        Task DeleteUser(int userId);

}
