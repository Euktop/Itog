using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog2.Classes
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public static int IdCounter { get; set; } = 100;

        protected BaseEntity()
        {
            Id = GenerateId();
        }
        public virtual string GenerateId()
        {
            IdCounter++;
            return IdCounter.ToString();
        }

    }
}
