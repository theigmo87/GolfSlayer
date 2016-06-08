using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Hole : EntityBase
    {
        public int SegmentID { get; set; }
        public int Number { get; set; }
        public int Par { get; set; }
        public int Distance { get; set; }
    }
}
