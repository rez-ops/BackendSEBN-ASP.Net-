using System;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;

        public CompensationService(ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
        }

        public async Task CreateCompensationAsync(Componsation componsation)
        {
            await _compensationRepository.CreateCompensation(componsation);
        }

        public async Task<List<Componsation?>?> getCompoCreateByUser(int userId)
        {
            return await _compensationRepository.getCompoCreateByUser(userId);
        }
    }
}
