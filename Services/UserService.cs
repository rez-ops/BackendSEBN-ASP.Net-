using System;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> UserExistsAsync(int matricule)
        {
            return await _userRepository.GetUserByMatriculeAsync(matricule);
        }

        //************login
        public async Task<User?> logIn(LoginModel loginModel)
        {
            int matricule = loginModel.Matricule;
            string password = loginModel.Password;

            // Await the result of the asynchronous method
            User? user = await UserExistsAsync(matricule);

            if (user != null && password == user.Password)
            {
                return user;
            }

            return null;
        }
        public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _userRepository.GetUserByIdAsync(userId);
    }

        public async Task<List<User?>?> getAllUsers()
        {
            return await _userRepository.getAllUsers();
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUser(int userId)
        {
            await _userRepository.DeleteUser(userId);
        }

        public async Task CreateUser(User user)
        {
            // You can add more validation if needed before calling the repository
            await _userRepository.CreateUser(user);
        }



    }
}
