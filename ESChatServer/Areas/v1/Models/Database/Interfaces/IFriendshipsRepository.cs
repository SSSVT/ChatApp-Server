using ESChatServer.Areas.v1.Models.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface IFriendshipsRepository : IRepository<Friendship>
    {
        ICollection<Friendship> FindAcceptedByUserID(long id);
        Task<ICollection<Friendship>> FindAcceptedByUserIDAsync(long id);

        ICollection<Friendship> FindPendingByUserID(long id);
        Task<ICollection<Friendship>> FindPendingByUserIDAsync(long id);
    }
}