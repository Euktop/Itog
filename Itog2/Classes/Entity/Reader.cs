using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog2.Classes
{
    public class Reader : BaseEntity
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string BookIda { get; set; }


        public override string ToString()
        {
            return $"{Id}#{Name}#{MiddleName}#{Surname}#{Phone}#{Email}#{Age}#{BookIda}";
        }
    }
}