using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RelationShips.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationShips
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initializer.Build();
            optionsBuilder.UseSqlServer(Initializer.Configuration.GetConnectionString("SqlServer"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cascade: master tablodan bir kayıt silindiğinde o kayda ait alt tablolardaki verilerde silinir.EFCore Default olarak böyle davranır
            //Restrict: master tablodan silinecek kayda ait alt tablolarda kayıtlar var ise kaydın silinmesini engelliyor.
            //SetNull: master tablodan silinecek kayda ait alt tablodaki kayıtlarda foreignkey nullable ise masterdaki kaydı silip alt tablolarda o kolona null set ediyor.
            //NoAction:hiçbir işlem yapmıyor. alt tabloların silme işlemini manuel yapmak için
            //*Client... ile başlayanlar ise veritabanında değil track edilen(memorydeki) data üzerinde bu işlemleri yapar
            //1->n ilişki ve foreignkey ataması fluentAPI yöntemi ile yapılır. her zaman has ile başla with ile devam et
            modelBuilder.Entity<Category>().HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.Category_Id).OnDelete(DeleteBehavior.Restrict);

            ////1->1 ilişkide hasforeignkey metoduna generic olarak hangi tablo foreignkey içerecek belirtmemiz gerekiyor.
            modelBuilder.Entity<Product>().HasOne(x => x.ProductFeature).WithOne(x => x.Product).HasForeignKey<ProductFeature>(x => x.ProductId).OnDelete(DeleteBehavior.SetNull);

            //n->n ilişki
            modelBuilder.Entity<Student>()
                .HasMany(x => x.Teachers).
                WithMany(x => x.Students).
                UsingEntity<Dictionary<string, object>>(
               "StudentTeacherWithManyToMany",
               x => x.HasOne<Teacher>().WithMany().HasForeignKey("TeacherId").HasConstraintName("FK_TeacherId").OnDelete(DeleteBehavior.NoAction),
               x => x.HasOne<Student>().WithMany().HasForeignKey("StudentId").HasConstraintName("FK_StudentId").OnDelete(DeleteBehavior.NoAction)
              );
            base.OnModelCreating(modelBuilder);
        }
    }
}
