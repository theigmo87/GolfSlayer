using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Segment : EntityBase
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
    }
}
