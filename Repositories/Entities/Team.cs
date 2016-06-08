using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Team : EntityBase
    {
        public string Pin { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}
