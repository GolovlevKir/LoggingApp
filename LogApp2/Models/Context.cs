using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogApp2.Models;

namespace LogApp2.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<NLogs> NLogs { get; set; }
        public DbSet<LogApp2.Models.mvcLoggingModel> mvcLoggingModel { get; set; }
        
    }
}
