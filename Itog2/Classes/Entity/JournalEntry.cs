using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog2.Classes
{
    public class JournalEntry
    {
        public string ReaderId { get; set; }
        public string BookId { get; set; }
        public string ISBN { get; set; }
        public string EmployeeId { get; set; }
        public string Type { get; set; } // "Выдача" или "Возврат"

        public override string ToString()
        {
            return $"{ReaderId}#{BookId}#{ISBN}#{EmployeeId}#{Type}";
        }
    }
}
