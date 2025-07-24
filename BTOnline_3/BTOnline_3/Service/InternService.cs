using BTOnline_3.DataConnection;
using BTOnline_3.IRepository;
using BTOnline_3.Models;
using Microsoft.EntityFrameworkCore;

namespace BTOnline_3.Service
{
    public class InternService : IRepoIntern
    {
        private readonly ApplicationDbContext _context;
        public InternService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<InternModel>> GetAllInternsAsync()
        {
            return await _context.InternsDb!.AsNoTracking().ToListAsync();
        }

        // Lấy danh sách Interns với các thuộc tính được lọc theo quyền truy cập(allowedProps danh sách)
        public async Task<List<Dictionary<string, object?>>> GetFilteredInternsAsync(HashSet<string> allowedProps)
        {
            var interns = await _context.InternsDb!.ToListAsync();

            var normalizedProps = allowedProps.Select(p => p.ToLower()).ToHashSet();
            var props = typeof(InternModel).GetProperties();

            return interns.Select(i =>
            {
                var dict = new Dictionary<string, object?>();
                foreach (var prop in props)
                {
                    if (normalizedProps.Contains(prop.Name.ToLower()))
                    {
                        dict[prop.Name] = prop.GetValue(i);
                    }
                }
                return dict;
            }).ToList();
        }


        public async Task<InternModel> GetInternByIdAsync(int id)
        {
            var intern = await _context.InternsDb!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return intern ?? throw new Exception($"Intern with ID {id} not found.");
        }
        public async Task<InternModel> CreateInternAsync(InternModel intern)
        {
            if (intern == null) throw new ArgumentNullException(nameof(intern));

            await _context.InternsDb!.AddAsync(intern);
            await _context.SaveChangesAsync();
            return intern;
        }

        public async Task<InternModel> UpdateInternAsync(InternModel intern)
        {
            var existingIntern = await _context.InternsDb!.FindAsync(intern.Id)
                ?? throw new Exception($"Intern with ID {intern.Id} not found.");

            // Cập nhật tất cả các thuộc tính
            existingIntern.InternName = intern.InternName;
            existingIntern.InternAddress = intern.InternAddress;
            existingIntern.ImageData = intern.ImageData;
            existingIntern.DateOfBirth = intern.DateOfBirth;
            existingIntern.InternMail = intern.InternMail;
            existingIntern.InternMailReplace = intern.InternMailReplace;
            existingIntern.University = intern.University;
            existingIntern.CitizenIdentification = intern.CitizenIdentification;
            existingIntern.CitizenIdentificationDate = intern.CitizenIdentificationDate;
            existingIntern.Major = intern.Major;
            existingIntern.Internable = intern.Internable;
            existingIntern.FullTime = intern.FullTime;
            existingIntern.Cvfile = intern.Cvfile;
            existingIntern.InternSpecialized = intern.InternSpecialized;
            existingIntern.TelephoneNum = intern.TelephoneNum;
            existingIntern.InternStatus = intern.InternStatus;
            existingIntern.RegisteredDate = intern.RegisteredDate;
            existingIntern.HowToKnowAlta = intern.HowToKnowAlta;
            existingIntern.InternPassword = intern.InternPassword;
            existingIntern.ForeignLanguage = intern.ForeignLanguage;
            existingIntern.YearOfExperiences = intern.YearOfExperiences;
            existingIntern.PasswordStatus = intern.PasswordStatus;
            existingIntern.ReadyToWork = intern.ReadyToWork;
            existingIntern.InternEnabled = intern.InternEnabled;
            existingIntern.EntranceTest = intern.EntranceTest;
            existingIntern.Introduction = intern.Introduction;
            existingIntern.Note = intern.Note;
            existingIntern.LinkProduct = intern.LinkProduct;
            existingIntern.JobFields = intern.JobFields;
            existingIntern.HiddenToEnterprise = intern.HiddenToEnterprise;

            _context.InternsDb.Update(existingIntern);
            await _context.SaveChangesAsync();

            return existingIntern;
        }

        public async Task<bool> DeleteInternAsync(int id)
        {
            var intern = await _context.InternsDb!.FindAsync(id);
            if (intern == null) return false;
            _context.InternsDb.Remove(intern);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
