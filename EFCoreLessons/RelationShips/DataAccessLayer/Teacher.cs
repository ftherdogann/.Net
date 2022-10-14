using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationShips.DataAccessLayer
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
    }
}
