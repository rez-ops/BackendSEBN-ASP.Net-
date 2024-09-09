using System;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IpermisionService
{
  Task CreatePermisionAsync(Permission permission);
  Task<List<Permission?>?> getPermissCreateByUser(int userId);
}
