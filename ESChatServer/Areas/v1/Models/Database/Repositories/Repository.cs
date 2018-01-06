using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Repositories
{
    public abstract class Repository<T>
    {
        public Repository(DatabaseContext context)
        {
            this._DatabaseContext = context;
        }

        protected DatabaseContext _DatabaseContext { get; set; }

        public virtual void SaveChanges()
        {
            this._DatabaseContext.SaveChanges();
        }
        public virtual async Task SaveChangesAsync()
        {
            await this._DatabaseContext.SaveChangesAsync();
        }
    }
}
