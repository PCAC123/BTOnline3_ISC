using BTOnline_3.Models;
using System.Threading.Tasks;

namespace BTOnline_3.IRepository
{
    public interface IRepoAllowAccess
    {
        Task<IEnumerable<AllowAccessModel>> GetAllAllowAccessesAsync();
        Task<AllowAccessModel> GetAllowAccessByIdAsync(int id);
        Task<AllowAccessModel> CreateAllowAccessAsync(AllowAccessModel allowAccess);
        Task<AllowAccessModel> UpdateAllowAccessAsync(AllowAccessModel allowAccess);
        Task<bool> DeleteAllowAccessAsync(int id);
        Task<AllowAccessModel?> GetAccessAsync(int roleId, string tableName);
        /// <summary>
        /// Additional methods can be added here as needed.
        /// </summary>
    }
}
