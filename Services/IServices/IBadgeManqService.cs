using System;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IBadgeManqService
{
  Task createBadgeManqAsync(BadgeManquant badgeManquant);

  Task<List<BadgeManquant?>?> getBadgeCreateByUser(int userId);
}
