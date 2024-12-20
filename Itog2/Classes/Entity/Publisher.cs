using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog2.Classes
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id}#{Name}";
        }
    }
}