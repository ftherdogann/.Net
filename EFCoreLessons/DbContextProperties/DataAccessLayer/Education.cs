using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextProperties.DataAccessLayer
{
    [Table ("EducationsWithDAAttributes") ]
    public class Education
    {
        //[Key] attribute ile Id dışındaki isimlendirmelerde primary key olduğunu belirtmemiz gerekiyor.
        [Column( Order = 1)]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        //[StringLength(250,MinimumLength = 100)]
        [Column("Name",TypeName ="varchar(250)",Order =4)]
        public string Name { get; set; }

        [Column("StartDate", Order = 2)]
        public DateTime StartDate { get; set; }

      
        [Column("EndDate", Order = 3)]
        public DateTime EndDate { get; set; }
    }
}
