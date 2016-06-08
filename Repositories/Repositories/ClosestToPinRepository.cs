using Repositories.Database;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IClosestToPinRepository : IRepositoryBase<ClosestToPin, int>
    { }
    public class ClosestToPinRepository : RepositoryBase<ClosestToPin, int>, IClosestToPinRepository
    {
        public ClosestToPinRepository(DataContext context) : base(context) { }
        public ClosestToPinRepository() : base() { }
    }
}
