using System;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class CongeService : ICongeService
    {
        private readonly ICongeRepositorie _congeRepository;
        private readonly IUserRepository _userRepository;

        public CongeService(ICongeRepositorie congeRepository, IUserRepository userRepository)
        {
            _congeRepository = congeRepository;
            _userRepository = userRepository;
        }

        public async Task CreateCongeAsync(Conge conge)
        {
            await _congeRepository.CreateConge(conge);
        }

        public Task<List<TypeConge?>> getAlTypeConge()
        {
            return _congeRepository.getAlTypeConge();
        }

        public Task<List<Conge?>?> getCongeCreateByUser(int userId)
        {
            return _congeRepository.getCongeCreateByUser(userId);
        }

        
    }
}
