using System;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IUserService
{
       


     //*********logIn

     Task<User?> logIn(LoginModel loginModel);
     Task<User?> UserExistsAsync(int matricule);
     Task<User?> GetUserByIdAsync(int userId);
     Task<List<User?>?> getAllUsers();

     Task UpdateUser(User user);
        Task DeleteUser(int userId);
        Task CreateUser(User user);

     

}
