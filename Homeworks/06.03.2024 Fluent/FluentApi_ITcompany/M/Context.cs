using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FluentApi_ITcompany.M
{
    public partial class Context : DbContext
    {

        static DbContextOptions<Context> _options;

        static Context()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            _options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;
        }
        public Context() : base(_options)
        {
            if (Database.EnsureCreated())
            {
                var manager = new Position { Name = "Manager" };
                var developer = new Position { Name = "Developer" };
                var designer = new Position { Name = "Designer" };

        
                Positions.Add(manager);
                Positions.Add(developer);
                Positions.Add(designer);

              
                var john = new Employee { Name = "John", Surname = "Doe", Position = manager };
                var alice = new Employee { Name = "Alice", Surname = "Smith", Position = developer };
                var bob = new Employee { Name = "Bob", Surname = "Johnson", Position = designer };
                var bob2 = new Employee { Name = "Bob2", Surname = "Johnson2", Position = designer };
                var bob3 = new Employee { Name = "Bob3", Surname = "Johnson3", Position = designer };

                Employees.Add(john);
                Employees.Add(alice);
                Employees.Add(bob);
                Employees.Add(bob2);
                Employees.Add(bob3);

               
                SaveChanges();
            }
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Surname).IsRequired();
                entity.HasOne(e => e.Position) 
                      .WithMany(p => p.Employees) 
                      .HasForeignKey(e => e.PosId);
            });

         
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(p => p.Id); 
                entity.Property(p => p.Name).IsRequired(); 
            });
        }

    }
}
