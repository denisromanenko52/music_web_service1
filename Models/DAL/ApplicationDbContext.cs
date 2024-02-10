using Microsoft.EntityFrameworkCore;
using music_web_service1.Models.Domain;
using System.Collections.Generic;

namespace music_web_service1.Models.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> User { get; set; }
        public DbSet<SearchHistory> SearchHistory { get; set; }
    }
}
