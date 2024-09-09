using System;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IBadgeManqRepository
{
  Task cerateBadgeManq(BadgeManquant badgeManquant);
  Task<List<BadgeManquant?>?> getBadgeCreateByUser(int userId);
}
