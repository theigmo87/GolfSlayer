using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Message : IEntity
    {
        public int ID { get; set; }
        public string Body { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public DateTime DateSent { get; set; }
    }
}
