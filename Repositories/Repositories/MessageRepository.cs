using Repositories.Database;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IMessageRepository : IRepositoryBase<Message, int>
    { }
    public class MessageRepository : RepositoryBase<Message, int>, IMessageRepository
    {
        public MessageRepository(DataContext context) : base(context) { }
        public MessageRepository() : base() { }
    }
}
