using BTOnline_3.Models;

namespace BTOnline_3.IRepository
{
    public interface IRepoRole
    {
        Task<IEnumerable<RoleModel>> GetAllRoleAsync();
        Task<RoleModel> GetByIdAsync(int id);
        
        Task<RoleModel> CreateRoleAsync(RoleModel role);
        Task<RoleModel> UpdateRoleAsync(RoleModel role);
        Task<bool> DeleteRoleAsync(int id);
        /// <summary>
        Task<IEnumerable<RoleModel>> GetAllRoleIdsAsync();
    }
}
