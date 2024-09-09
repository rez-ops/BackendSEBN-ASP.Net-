using System;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Repositories.IRepositories;

namespace WebApplication1.Services
{
    public class ChangHoraService : IChangHoraService
    {
        private readonly IChangHoraRepository _changHoraRepository;

        public ChangHoraService(IChangHoraRepository changHoraRepository)
        {
            _changHoraRepository = changHoraRepository;
        }

        public async Task CreateChangHoraAsync(ChangementHoraire changementHoraire)
        {
            await _changHoraRepository.CreateChangHora(changementHoraire);
        }

        public async Task<List<ChangementHoraire?>?> getChangHorCreateByUser(int userId)
        {
            return await _changHoraRepository.getChangHorCreateByUser(userId);
        }
    }
}
