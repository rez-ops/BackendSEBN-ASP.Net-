using System;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class PermisionService : IpermisionService
    {
        private readonly IPermissionRepository _permisionRepository;

        public PermisionService(IPermissionRepository permissionRepository)
        {
            _permisionRepository = permissionRepository;
        }

        public async Task CreatePermisionAsync(Permission permission)
        {
            await _permisionRepository.CreatePermission(permission);
        }

        public async Task<List<Permission?>?> getPermissCreateByUser(int userId)
        {
            return await _permisionRepository.getPermissCreateByUser(userId);
        }
    }
}
