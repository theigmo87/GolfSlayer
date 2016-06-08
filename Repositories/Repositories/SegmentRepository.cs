using Repositories.Database;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ISegmentRepository : IRepositoryBase<Segment, int>
    { }
    public class SegmentRepository : RepositoryBase<Segment, int>, ISegmentRepository
    {
        public SegmentRepository(DataContext context) : base(context) { }
        public SegmentRepository() : base() { }
    }
}
