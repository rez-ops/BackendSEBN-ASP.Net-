using System;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IChangHoraService
{
  Task CreateChangHoraAsync(ChangementHoraire changementHoraire);
  Task<List<ChangementHoraire?>?> getChangHorCreateByUser(int userId);
}
