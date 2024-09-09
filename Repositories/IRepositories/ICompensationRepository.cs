using System;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface ICompensationRepository
{
  Task CreateCompensation(Componsation componsation);
  Task<List<Componsation?>?> getCompoCreateByUser(int userId);
}
