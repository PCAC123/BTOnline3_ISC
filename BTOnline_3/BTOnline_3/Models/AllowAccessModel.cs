namespace BTOnline_3.Models
{
    public class AllowAccessModel
    {
        public AllowAccessModel() { }
        public AllowAccessModel(int accessId, int roleId, string tableName, string accessProperties)
        {
            AccessId = accessId;
            RoleId = roleId;
            TableName = tableName;
            AccessProperties = accessProperties;
        }

        /// <summary>
        /// Unique identifier for the access rule. (Primary Key)
        /// </summary>
        public int AccessId { get; set; }

        /// <summary>
        /// Foreign Key linking to the Role table.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// The name of the table to which access is being granted (e.g., "Intern", "User").
        /// </summary>
        public string TableName { get; set; } = string.Empty; // Not nullable

        /// <summary>
        /// A comma-separated string of property names that the role can access within the TableName.
        /// Example: "Id,InternName,InternMail"
        /// </summary>
        public string AccessProperties { get; set; } = string.Empty; // Not nullable

        // Navigation property (optional, for ORMs like Entity Framework)
        // public virtual Role? Role { get; set; }
    }
}
