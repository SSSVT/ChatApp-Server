using System.Threading.Tasks;
using ESChatServer.Areas.v1.Models.Database.Entities;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface IPasswordResetRepository
    {
        Task AddAsync(PasswordReset item, bool saveChanges);
        Task<bool> ExistsAsync(object id);
        Task<PasswordReset> FindAsync(object id);
        Task<PasswordReset> FindValidAsync(object id);
        Task UpdateAsync(PasswordReset item, bool saveChanges);
    }
}