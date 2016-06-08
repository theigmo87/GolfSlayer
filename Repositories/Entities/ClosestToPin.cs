using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class ClosestToPin : EntityBase
    {        
        public string Name { get; set; }
        public decimal Distance { get; set; }
        public int HoleID { get; set; }        
    }
}
