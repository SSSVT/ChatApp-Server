using ESChatServer.Areas.v1.Models.Database.Entities;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        User FindByUsername(string username);
        Task<User> FindByUsernameAsync(string username);
    }
}