using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog2.Classes
{
    public class Book : BaseEntity
    {
        public string AuthorIda { get; set; }
        public string GenreIda { get; set; }
        public string PublisherIda { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id}#{AuthorIda}#{GenreIda}#{PublisherIda}#{Name}";
        }
    }
}
