using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Repositories
{
    public class PasswordResetRepository : Repository<PasswordReset>, IPasswordResetRepository
    {
        public PasswordResetRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<PasswordReset> FindAsync(object id)
        {
            return await this._DatabaseContext.PasswordResets.FindAsync(id);
        }

        public async Task AddAsync(PasswordReset item, bool saveChanges)
        {
            await this._DatabaseContext.PasswordResets.AddAsync(item);

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public async Task UpdateAsync(PasswordReset item, bool saveChanges)
        {
            PasswordReset reset = await this.FindAsync(item.Id);
            reset.UtcExpiration = item.UtcExpiration;

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(object id)
        {
            return await this.FindAsync(id) != null;
        }

        public async Task<PasswordReset> FindValidAsync(object id)
        {
            PasswordReset reset = await this.FindAsync(id);
            if (reset.UtcExpiration >= DateTime.UtcNow && !reset.Used)
            {
                return reset;
            }
            return null;
        }
    }
}
