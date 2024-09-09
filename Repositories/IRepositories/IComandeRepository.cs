using System;
using WebApplication1.Models;

namespace WebApplication1.Repositories.IRepositories;

public interface IComandeRepository
{
  Task CreateComande(Comande comande);
  Task<List<Comande>>GetAllComandesByUserId(int id);
  Task<Comande> GetComandeCreebyId(int id);
}
