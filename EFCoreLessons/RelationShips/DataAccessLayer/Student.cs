using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationShips.DataAccessLayer
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }

        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
