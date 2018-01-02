using ESChatServer.Areas.v1.Models.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface IFriendshipsRepository : IRepository<Friendship>
    {
        ICollection<Friendship> FindByUserID(long id);
        Task<ICollection<Friendship>> FindByUserIDAsync(long id);

        ICollection<Friendship> FindRequestsByUserID(long id);
        Task<ICollection<Friendship>> FindRequestsByUserIDAsync(long id);
    }
}