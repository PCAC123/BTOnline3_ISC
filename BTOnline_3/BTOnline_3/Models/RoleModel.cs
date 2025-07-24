using System.Text.Json.Serialization;

namespace BTOnline_3.Models
{
    public class RoleModel
    {
        public RoleModel() { }
        public RoleModel(int roleId, string? roleName)
        {
            RoleId = roleId;
            RoleName = roleName ?? null;
        }       
        
        /// <summary>
        /// Unique identifier for the role. (Primary Key)
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Name of the role (e.g., "Admin", "HR", "Intern Manager", "Student").
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RoleName { get; set; } = string.Empty; // Not nullable        
    }
}
