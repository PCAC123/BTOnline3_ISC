using System.ComponentModel.DataAnnotations;

namespace BTOnline_3.Models
{
    public class UserModel
    {
        public UserModel() { }
        public UserModel(int userId, string? fullName, string? email, 
            string? passwordHash, string? phoneNumber, DateTime? dateOfBirth, 
            string? address, string? gender, int? roleId, bool? isActive, DateTime? lastLoginDate)
        {
            UserId = userId;
            FullName = fullName;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Address = address;
            Gender = gender;
            RoleId = roleId;
            IsActive = isActive;
            LastLoginDate = lastLoginDate;
        }
        public UserModel(string? emailLogin , string? passwordHashLogin)
        {
            Email = emailLogin;
            PasswordHash = passwordHashLogin;
        }
        /// <summary>
        /// Unique identifier for the user. (Primary Key)
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Email address of the user (used for login).
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Hashed password for the user. This should be securely stored.
        /// </summary>
        public string? PasswordHash { get; set; } = string.Empty; // Not nullable, should always have a hash

        /// <summary>
        /// Phone number of the user.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Date of birth of the user.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Address of the user.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gender of the user.
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// Foreign Key linking to the Role table, defining the user's role.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// Activation status of the user account.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Last login timestamp of the user.
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        // Navigation property (optional, for ORMs like Entity Framework)
        //public virtual RoleModel? Role { get; set; }
    }

}
