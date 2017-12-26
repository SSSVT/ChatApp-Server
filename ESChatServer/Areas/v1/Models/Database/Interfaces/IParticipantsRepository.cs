using ESChatServer.Areas.v1.Models.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface IParticipantsRepository : IRepository<Participant>
    {
        ICollection<Participant> FindByUserID(long id);
        Task<ICollection<Participant>> FindByUserIDAsync(long id);

        ICollection<Participant> FindByRoomID(long id);
        Task<ICollection<Participant>> FindByRoomIDAsync(long id);
    }
}