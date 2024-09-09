using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class CongeRepositorie :ICongeRepositorie
{
  private readonly ApplicationDbContext _context;
  public CongeRepositorie(ApplicationDbContext context)
  {
    _context = context;
  }

    public async Task CreateConge(Conge conge)
    {
        await _context.AddAsync(conge);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Conge?>> getCongeCreateByUser(int userId)
{
    return await _context.Conges
                         .Where(c => c.UserId == userId)  // Filter by userId
                         .ToListAsync();   
}
public async Task<List<TypeConge?>> getAlTypeConge()
{
    return await _context.TypeConges
                         .ToListAsync();
}



}
