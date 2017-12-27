using ESChatServer.Areas.v1.Models.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface IMessagesRepository : IRepository<Message>
    {
        ICollection<Message> FindByUserID(long id);
        Task<ICollection<Message>> FindByUserIDAsync(long id);

        ICollection<Message> FindByRoomID(long id);
        Task<ICollection<Message>> FindByRoomIDAsync(long id);
    }
}