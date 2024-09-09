using System;
using WebApplication1.Models;

namespace WebApplication1.Services.IServices;

public interface IComandeService
{
  Task CreateComande(Comande comande);
  Task<List<Comande>>GetAllComandesByUserId(int id);
  Task<Comande> GetComandeCreebyId(int id);
}
