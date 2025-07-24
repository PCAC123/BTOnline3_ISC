using BTOnline_3.DataConnection;
using BTOnline_3.IRepository;
using BTOnline_3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace BTOnline_3.Service
{
    public class RoleService : IRepoRole
    {
        private readonly ApplicationDbContext _context;
        public RoleService(ApplicationDbContext context) => _context = context;
        public async Task<IEnumerable<RoleModel>> GetAllRoleAsync()
        {
            return await _context.RolesDb!.AsNoTracking().ToListAsync();
        }
        public async Task<RoleModel> GetByIdAsync(int id)
        {
            var role = await _context.RolesDb!.AsNoTracking().FirstOrDefaultAsync(x => x.RoleId == id);

            return role ?? throw new Exception($"Role with ID {id} not found.");
        }
        public async Task<IEnumerable<RoleModel>> GetAllRoleIdsAsync()
        {
            // Select only the RoleId property and convert to a list.
            // AsNoTracking() is used as this is a read-only projection.
            var listRoleId = await _context.RolesDb!.AsNoTracking().Select(r => r.RoleId).ToListAsync();
            return listRoleId.Select(id => new RoleModel { RoleId = id , RoleName=null }).ToList();
            //var result = await _context.RolesDb!
            // .AsNoTracking()
            // .Select(r => new RoleModel(r.RoleId))
            // .ToListAsync();

            //return result;
        }
        public async Task<RoleModel> CreateRoleAsync(RoleModel role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            await _context.RolesDb!.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }
        public async Task<RoleModel> UpdateRoleAsync(RoleModel role)
        {
            var existingRole = await _context.RolesDb!.FindAsync(role.RoleId) ?? throw new Exception($"Role with ID {role.RoleId} not found.");
            existingRole.RoleName = role.RoleName;
            // Cập nhật các field khác nếu có

            _context.RolesDb.Update(existingRole);
            await _context.SaveChangesAsync();

            return existingRole;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.RolesDb!.FindAsync(id);
            if (role == null) return false;

            _context.RolesDb.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
