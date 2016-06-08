using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Score : EntityBase
    {
        public int TeamID { get; set; }
        public int HoleID { get; set; }
        public int Value { get; set; }        
    }
}
