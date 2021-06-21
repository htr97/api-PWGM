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
        public DbSet<Ubication> Ubications {get; set;}
        public DbSet<Company> Companies {get; set;}
        public DbSet<Priority> Priorities {get; set;}
        public DbSet<Maintenance> Maintenances {get; set;}
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Payment> Payments { get; set; }
    } 
}