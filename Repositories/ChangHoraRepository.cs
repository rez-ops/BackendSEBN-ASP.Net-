using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repositories.IRepositories;

namespace WebApplication1.Repositories;

public class ChangHoraRepository : IChangHoraRepository
{

  private readonly ApplicationDbContext _context;
  public ChangHoraRepository(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task CreateChangHora(ChangementHoraire changementHoraire)
    {
        await _context.AddAsync(changementHoraire);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ChangementHoraire?>?> getChangHorCreateByUser(int userId)
    {
        return await _context.ChangementHoraires
                              .Where(c => c.UserId == userId)
                              .ToListAsync();
    }
}
