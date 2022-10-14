using DbContextProperties.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextProperties
{
    public class AppDbContext :DbContext
    {
        public DbSet<Product> ProductsItem { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            optionsBuilder.UseSqlServer(Initializer.Configuration.GetConnectionString("SqlServer"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().HasKey(x => x.Invoice_Id);
            modelBuilder.Entity<Invoice>().Property(x => x.InvoiceNumber).IsRequired();
            modelBuilder.Entity<Invoice>().Property(x => x.Description).HasMaxLength(150);
            modelBuilder.Entity<Invoice>().Property(x => x.Description).IsFixedLength();
            modelBuilder.Entity<Invoice>().ToTable("InvoiceWithFluentAPI");          
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            //ortak olan yapıları burada kurabiliriz
            ChangeTracker.Entries().ToList().ForEach(a =>
            {
                if (a.Entity is Product x)
                {
                    if (a.State == EntityState.Added)
                    {
                        x.CreatedDate = DateTime.Now;
                    }
                }
            });
            return base.SaveChanges();
        }
    }
}
