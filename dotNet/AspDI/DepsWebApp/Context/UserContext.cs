using System;
using DepsWebApp.Contracts;
using DepsWebApp.Services;
using Microsoft.EntityFrameworkCore;

namespace DepsWebApp.Context
{
    /// <summary>
    /// Implements <see cref="IUserService"/>>
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Database context options</param>
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// Configuring
        /// </summary>
        /// <param name="optionsBuilder">Option builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}