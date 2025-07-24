using BTOnline_3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using static BTOnline_3.Models.UserModel;

namespace BTOnline_3.DataConnection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for the Intern entity.
        /// </summary>
        public DbSet<InternModel>? InternsDb { get; set; }

        /// <summary>
        /// DbSet for the User entity.
        /// Note: The table name in SQL Server is '[User]' due to 'USER' being a reserved keyword.
        /// </summary>
        public DbSet<UserModel>? UsersDb { get; set; }

        /// <summary>
        /// DbSet for the Role entity.
        /// </summary>
        public DbSet<RoleModel>? RolesDb { get; set; }

        /// <summary>
        /// DbSet for the AllowAccess entity.
        /// </summary>
        public DbSet<AllowAccessModel>? AllowAccessesDb { get; set; }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types.
        /// This method is called by the framework when the model is being initialized.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the 'User' entity to map to the '[User]' table in SQL Server
            // because 'USER' is a reserved keyword.
            modelBuilder.Entity<UserModel>().ToTable("User");

            // Configure the primary key for Intern
            modelBuilder.Entity<InternModel>()
                .HasKey(i => i.Id);

            // Configure the primary key for User
            modelBuilder.Entity<UserModel>()
                .HasKey(u => u.UserId);

            // Configure the primary key for Role
            modelBuilder.Entity<RoleModel>()
                .HasKey(r => r.RoleId);

            // Configure the primary key for AllowAccess
            modelBuilder.Entity<AllowAccessModel>()
                .HasKey(a => a.AccessId);

            // Configure the one-to-many relationship between Role and User
            // A Role can have many Users, and a User belongs to one Role.
            modelBuilder.Entity<UserModel>()
                .HasOne<RoleModel>() // User has one Role
                .WithMany()     // Role can have many Users (no navigation property on Role side)
                .HasForeignKey(u => u.RoleId) // Foreign key in User table
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for roles

            // Configure the one-to-many relationship between Role and AllowAccess
            // A Role can have many AllowAccess rules, and an AllowAccess rule belongs to one Role.
            modelBuilder.Entity<AllowAccessModel>()
                .HasOne<RoleModel>() // AllowAccess has one Role
                .WithMany()     // Role can have many AllowAccess rules (no navigation property on Role side)
                .HasForeignKey(a => a.RoleId) // Foreign key in AllowAccess table
                .OnDelete(DeleteBehavior.Cascade); // If a role is deleted, its access rules are also deleted
        }
    }
}
