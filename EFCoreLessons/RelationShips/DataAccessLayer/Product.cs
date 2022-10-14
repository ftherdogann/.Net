using RelationShips.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationShips
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Barcode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; } = DateTime.Now;


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastAccessDate { get; set; } = DateTime.Now;

        public ProductFeature ProductFeature { get; set; }

        //CategoryId olarak belirtilirse direk ef core foreignkey olarak set eder.böyle yapılırsa OnModelCreating üzerinde belirtmeye gerek yok.
        public int Category_Id { get; set; } 

        //[ForeignKey("Category_Id")] //Bu kod ile de foreignkey ataması yapıp category tablosu ile ilişki kurulabilir.
        public Category Category { get; set; }

        //database üzerinde bir kolon var ancak classta onu temsil edecek property yok ise o property shadow property olarak isimlendirilir.
    }
}
