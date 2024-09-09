using System;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class BadgeManqService :IBadgeManqService
    {
        private readonly IBadgeManqRepository _badgeManqRepository;

        public BadgeManqService(IBadgeManqRepository badgeManqRepository)
        {
            _badgeManqRepository = badgeManqRepository;
        }

        public async Task createBadgeManqAsync(BadgeManquant badgeManquant)
        {
            await _badgeManqRepository.cerateBadgeManq(badgeManquant);
        }

        public async Task<List<BadgeManquant?>?> getBadgeCreateByUser(int userId)
        {
            return await _badgeManqRepository.getBadgeCreateByUser(userId);
        }
    }
}
