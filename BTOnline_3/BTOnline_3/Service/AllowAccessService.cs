using BTOnline_3.DataConnection;
using BTOnline_3.IRepository;
using BTOnline_3.Models;
using Microsoft.EntityFrameworkCore;

namespace BTOnline_3.Service
{
    public class AllowAccessService : IRepoAllowAccess
    {
        private readonly ApplicationDbContext _context;
        public AllowAccessService(ApplicationDbContext context)
        {
            _context = context;
        }   
        public async Task<IEnumerable<AllowAccessModel>> GetAllAllowAccessesAsync()
        {
            return await _context.AllowAccessesDb!.AsNoTracking().ToListAsync();
        }
        public async Task<AllowAccessModel?> GetAccessAsync(int roleId, string tableName)
        {
            return await _context!.AllowAccessesDb!
                    .AsNoTracking()
                .FirstOrDefaultAsync(a =>
                    a.RoleId == roleId &&
                    a.TableName.ToLower() == tableName.ToLower());
        }
        public async Task<AllowAccessModel> GetAllowAccessByIdAsync(int id)
        {
            var allowAccess = await _context.AllowAccessesDb!.AsNoTracking().FirstOrDefaultAsync(x => x.AccessId == id);
            return allowAccess ?? throw new Exception($"AllowAccess with ID {id} not found.");
        }
        public async Task<AllowAccessModel> CreateAllowAccessAsync(AllowAccessModel allowAccess)
        {
            if (allowAccess == null) throw new ArgumentNullException(nameof(allowAccess));
            await _context.AllowAccessesDb!.AddAsync(allowAccess);
            await _context.SaveChangesAsync();
            return allowAccess;
        }
        public async Task<AllowAccessModel> UpdateAllowAccessAsync(AllowAccessModel allowAccess)
        {
            var existingAllowAccess = await _context.AllowAccessesDb!.FindAsync(allowAccess.AccessId) 
                ?? throw new Exception($"AllowAccess with ID {allowAccess.AccessId} not found.");
            existingAllowAccess.RoleId = allowAccess.RoleId;
            existingAllowAccess.TableName = allowAccess.TableName;
            existingAllowAccess.AccessProperties = allowAccess.AccessProperties;
            // Update other fields as necessary
            _context.AllowAccessesDb.Update(existingAllowAccess);
            await _context.SaveChangesAsync();
            return existingAllowAccess;
        }
        public async Task<bool> DeleteAllowAccessAsync(int id)
        {
            var allowAccess = await _context.AllowAccessesDb!.FindAsync(id);
            if (allowAccess == null) return false;
            _context.AllowAccessesDb.Remove(allowAccess);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
