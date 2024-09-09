using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repositories.IRepositories;

namespace WebApplication1.Repositories;

public class ComandeRepository : IComandeRepository
{   private readonly ApplicationDbContext _context;
  public ComandeRepository(ApplicationDbContext context)
  {
    _context = context;
  }
    public async Task<Comande> GetComandeCreebyId(int id)
{
    return await _context.comandes
                         .Include(c => c.Articles) // Ensure Articles are included
                         .FirstOrDefaultAsync(c => c.Id == id);
}

public async Task<List<Comande>> GetAllComandesByUserId(int userId)
{
    return await _context.comandes
                         .Include(c => c.Articles) // Ensure Articles are included
                         .Where(c => c.UserId == userId)
                         .ToListAsync();
}

public async Task CreateComande(Comande comande)
{
    // Attach existing articles to the Comande
    foreach (var article in comande.Articles)
    {
        _context.Entry(article).State = EntityState.Unchanged;
    }

    // Add the Comande to the context
    await _context.comandes.AddAsync(comande);

    // Save changes
    await _context.SaveChangesAsync();
}


}