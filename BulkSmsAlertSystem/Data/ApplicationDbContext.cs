using Microsoft.EntityFrameworkCore;
using BulkSmsAlertSystem.Models;
using System.Collections.Generic;

namespace BulkSmsAlertSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SmsMessage> SmsMessages { get; set; }
    }
}
