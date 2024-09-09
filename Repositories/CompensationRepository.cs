using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class CompensationRepository:ICompensationRepository
{
  private readonly ApplicationDbContext _context;
  public CompensationRepository(ApplicationDbContext context)
  {
    _context = context;
  }

    public async Task CreateCompensation(Componsation componsation)
    {
        await _context.AddAsync(componsation);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Componsation?>?> getCompoCreateByUser(int userId)
    {
        return await _context.Componsations
                              .Where(c => c.UserId == userId)
                              .ToListAsync();
    }
}
