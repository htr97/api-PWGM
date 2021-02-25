using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options): base(options){

        }

        public DbSet<AppUser> Users {get; set;}
        public DbSet<MaintenanceType> MaintenanceTypes {get; set;}
        public DbSet<Problem> Problems {get; set;}
    }
}