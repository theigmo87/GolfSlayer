using Repositories.Database;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IScoreRepository : IRepositoryBase<Score, int>
    { }
    public class ScoreRepository : RepositoryBase<Score, int>, IScoreRepository
    {
        public ScoreRepository(DataContext context) : base(context) { }
        public ScoreRepository() : base() { }
    }
}
