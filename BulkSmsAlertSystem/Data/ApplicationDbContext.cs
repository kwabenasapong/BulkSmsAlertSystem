using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BulkSmsAlertSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkSmsAlertSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SmsMessage> SmsMessages { get; set; }
    }
}

