using BTOnline_3.DataConnection;
using BTOnline_3.IRepository;
using BTOnline_3.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BTOnline_3.Service
{
    public class UserService : IRepoUser
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _context.UsersDb!.AsNoTracking().ToListAsync();
        }
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            var user = await _context.UsersDb!.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
            return user ?? throw new Exception($"User with ID {id} not found.");
        }


        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new ArgumentException("Password cannot be null or empty.", nameof(user.PasswordHash));
            // Hash the password before saving
            user.PasswordHash = HmacSHA256(user.PasswordHash);

            await _context.UsersDb!.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<UserModel> UpdateUserAsync(UserModel user)
        {
            var existingUser = await _context.UsersDb!.FindAsync(user.UserId)
                ?? throw new Exception($"User with ID {user.UserId} not found.");

            // Cập nhật các thông tin cần thiết (chỉ nếu có thay đổi)
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.Address = user.Address;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.Gender = user.Gender;
            existingUser.RoleId = user.RoleId;
            existingUser.IsActive = user.IsActive;
            existingUser.LastLoginDate = user.LastLoginDate;

            existingUser.PasswordHash = HmacSHA256(user.PasswordHash!);

            _context.UsersDb.Update(existingUser);
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.UsersDb!.FindAsync(id);
            if (user == null) return false;
            _context.UsersDb.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<UserModel?> AuthenticateUserAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Email and password cannot be null or empty.");
            string hasspassword = HmacSHA256(password);
            // Lưu ý: nếu bạn hash password thì cần giải mã/hàm so sánh tương ứng ở đây
            var user = await _context.UsersDb!
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == hasspassword);

            return user; // null nếu sai
        }
        /// <summary>
        /// void password to HMAC SHA256
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string HmacSHA256(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = SHA256.HashData(bytes); // .NET 6+
            return Convert.ToBase64String(hash);
        }
        public async Task ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var user = await _context.UsersDb!.FindAsync(userId)
                ?? throw new Exception("User not found.");

            if (user.PasswordHash != HmacSHA256(oldPassword))
                throw new UnauthorizedAccessException("Old password is incorrect.");

            user.PasswordHash = HmacSHA256(newPassword);
            _context.UsersDb.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
