using Microsoft.EntityFrameworkCore;
using NYSM.Models;

namespace NYSM.Data
{
    public class NYSMContext : DbContext
    {
        public NYSMContext(DbContextOptions<NYSMContext> opt) : base(opt)
        {
            
        }
        public DbSet<User> users {get ; set;}
        public DbSet<AppFile> appFiles {get ; set;}
        public DbSet<ReadSpeed> readSpeeds {get ; set;}
        public DbSet<TestConfig> testConfigs {get ; set;}
        public DbSet<TestResult> testResults {get ; set;}
        public DbSet<Question> questions {get ; set;}
    }
}