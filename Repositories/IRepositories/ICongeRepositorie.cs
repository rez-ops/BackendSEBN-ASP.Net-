using System;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface ICongeRepositorie
{
  Task CreateConge(Conge conge);
  Task<List<Conge?>?> getCongeCreateByUser(int userId);
  Task<List<TypeConge?>> getAlTypeConge();
}
