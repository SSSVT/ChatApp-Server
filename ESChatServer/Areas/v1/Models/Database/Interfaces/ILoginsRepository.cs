using ESChatServer.Areas.v1.Models.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface ILoginsRepository : IRepository<Login>
    {
        ICollection<Login> FindByUserID(long id);
        Task<ICollection<Login>> FindByUserIDAsync(long id);
    }
}