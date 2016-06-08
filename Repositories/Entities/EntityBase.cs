using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public interface IEntity
    {
        int ID { get; set; }        
    }
    public abstract class EntityBase : IEntity
    {
        public int ID { get; set; }        
        public DateTime DateInserted { get; set; }
        public DateTime DateUpdated { get; set; }        
    }
}
