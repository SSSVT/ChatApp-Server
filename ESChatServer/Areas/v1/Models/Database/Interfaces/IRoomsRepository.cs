using ESChatServer.Areas.v1.Models.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface IRoomsRepository : IRepository<Room>
    {
        ICollection<Room> FindByUserID(long id);
        Task<ICollection<Room>> FindByUserIDAsync(long id);
    }
}