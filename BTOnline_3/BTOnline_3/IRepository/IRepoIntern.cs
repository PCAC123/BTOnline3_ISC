using BTOnline_3.Models;

namespace BTOnline_3.IRepository
{
    public interface IRepoIntern
    {
        Task<IEnumerable<InternModel>> GetAllInternsAsync();
        Task<InternModel> GetInternByIdAsync(int id);
        Task<InternModel> CreateInternAsync(InternModel intern);
        Task<InternModel> UpdateInternAsync(InternModel intern);
        Task<bool> DeleteInternAsync(int id);
        Task<List<Dictionary<string, object?>>> GetFilteredInternsAsync(HashSet<string> allowedProps);
    }
}
