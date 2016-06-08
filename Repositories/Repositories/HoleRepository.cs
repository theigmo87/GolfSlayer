using Repositories.Database;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IHoleRepository : IRepositoryBase<Hole, int>
    { }
    public class HoleRepository : RepositoryBase<Hole, int>, IHoleRepository
    {
        public HoleRepository(DataContext context) : base(context) { }
        public HoleRepository() : base() { }
    }
}
