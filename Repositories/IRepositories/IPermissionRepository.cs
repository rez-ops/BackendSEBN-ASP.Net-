using System;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IPermissionRepository
{
  Task CreatePermission(Permission permission);
  Task<List<Permission?>?> getPermissCreateByUser(int userId);
}
