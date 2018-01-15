using ESChatServer.Areas.v1.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface IMessagesRepository : IRepository<Message>
    {
        ICollection<Message> FindByUserID(long id);
        Task<ICollection<Message>> FindByUserIDAsync(long id);

        ICollection<Message> FindByRoomID(long id, DateTime time);
        Task<ICollection<Message>> FindByRoomIDAsync(long id, DateTime time);
    }
}