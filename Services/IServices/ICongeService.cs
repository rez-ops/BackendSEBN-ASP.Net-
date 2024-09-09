using System;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ICongeService
{
  //Task<bool> UserExistsAsync(int userId);
  Task CreateCongeAsync(Conge conge);
  Task<List<Conge?>?> getCongeCreateByUser(int userId);
  Task<List<TypeConge?>> getAlTypeConge();
}
