using Repositories.Database;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ITeamRepository : IRepositoryBase<Team, int>
    { }
    public class TeamRepository : RepositoryBase<Team, int>, ITeamRepository
    {
        public TeamRepository(DataContext context) : base(context) { }
        public TeamRepository() : base() { }
    }
}
