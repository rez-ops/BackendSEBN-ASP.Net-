using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class PermissionRepository:IPermissionRepository
{
  private readonly ApplicationDbContext _context;
  public PermissionRepository(ApplicationDbContext context)
  {
    _context = context;
  }

    public async Task CreatePermission(Permission permission)
    {
        await _context.AddAsync(permission);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Permission?>?> getPermissCreateByUser(int userId)
    {
        return await _context.Permissions
                      .Where(c => c.UserId == userId)
                      .ToListAsync();
    }
}
