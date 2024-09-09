using System;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ICompensationService
{
  Task CreateCompensationAsync(Componsation componsation);
  Task<List<Componsation?>?> getCompoCreateByUser(int userId);
}
