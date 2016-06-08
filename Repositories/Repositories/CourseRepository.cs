using Repositories.Database;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICourseRepository : IRepositoryBase<Course, int>
    { }
    public class CourseRepository : RepositoryBase<Course, int>, ICourseRepository
    {
        public CourseRepository(DataContext context) : base(context) { }
        public CourseRepository() : base() { }
    }
}
