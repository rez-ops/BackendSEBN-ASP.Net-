using System;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Repositories.IRepositories;
using WebApplication1.Services.IServices;

namespace WebApplication1.Services;

public class ComandeService:IComandeService
{
  private readonly IComandeRepository _comandeRepository;
  public ComandeService(IComandeRepository comandeRepository){
    _comandeRepository=comandeRepository;
  }

    public async Task CreateComande(Comande comande)
{
    await _comandeRepository.CreateComande(comande);
}

public async Task<List<Comande>> GetAllComandesByUserId(int userId)
{
    return await _comandeRepository.GetAllComandesByUserId(userId);
}

public async Task<Comande> GetComandeCreebyId(int id)
{
    return await _comandeRepository.GetComandeCreebyId(id);
}

}
