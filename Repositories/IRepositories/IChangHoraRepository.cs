using System;
using WebApplication1.Models;

namespace WebApplication1.Repositories.IRepositories;

public interface IChangHoraRepository
{
  Task CreateChangHora(ChangementHoraire changementHoraire);
  Task<List<ChangementHoraire?>?> getChangHorCreateByUser(int userId);
}
