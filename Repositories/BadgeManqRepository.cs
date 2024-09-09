using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class BadgeManqRepository:IBadgeManqRepository
{
  private readonly ApplicationDbContext _context;
  public BadgeManqRepository(ApplicationDbContext context)
  {
    _context = context;
  }

    public async Task cerateBadgeManq(BadgeManquant badgeManquant)
    {
        await  _context.AddAsync(badgeManquant);
        await  _context.SaveChangesAsync();
    }

    public Task<List<Conge?>?> getBadgeCreateByUser(int userId)
    {
        throw new NotImplementedException();
    }

   

    async Task<List<BadgeManquant?>?> IBadgeManqRepository.getBadgeCreateByUser(int userId)
    {
        return await _context.BadgeManquants
                              .Where(c => c.UserId == userId)
                              .ToListAsync();
    }



    
}
