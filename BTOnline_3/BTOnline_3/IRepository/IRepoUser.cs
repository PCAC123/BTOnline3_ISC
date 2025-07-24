using BTOnline_3.Models;

namespace BTOnline_3.IRepository
{
    public interface IRepoUser
    {        
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> CreateUserAsync(UserModel user);
        Task<UserModel> UpdateUserAsync(UserModel user);
        Task<bool> DeleteUserAsync(int id);
        Task<UserModel?> AuthenticateUserAsync(string email, string password);
        /// <summary>
        /// Additional methods can be added here as needed.
        /// </summary>
    }
}
