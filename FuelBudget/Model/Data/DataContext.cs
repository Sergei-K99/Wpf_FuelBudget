using Microsoft.EntityFrameworkCore;

namespace FuelBudget.Model.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<FuelDetail> FuelDetails { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentButget> DepartmentButgets { get; set; }
        public DbSet<MeasuringPoint> MeasuringPoints { get; set; }
        
        public DataContext(){
            //Database.EnsureDeleted();
            Database.EnsureCreated(); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite("Data Source="+ "E:\\C#\\WPF\\Fuel_budget\\FuelBudget\\FuelBudget\\DB_Test1.db");
        }
    }
}

