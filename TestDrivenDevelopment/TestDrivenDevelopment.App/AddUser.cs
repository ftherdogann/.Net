using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestDrivenDevelopment.Apps
{
    public class AddUser
    {
        public bool UserAdd(string name, string phone, string email)
        {
            if (name.Length < 4) return false;
            if (!Regex.IsMatch(phone, "[0-9]")) return false;
            if (!email.Contains("@")) return false;

            return true;
        }
    }
}
