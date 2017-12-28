using ESChatServer.Areas.v1.Models.Database.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Repositories
{
    public abstract class Repository<T> : IRepository<T>
    {
        public Repository(DatabaseContext context)
        {
            this._DatabaseContext = context;
        }

        protected DatabaseContext _DatabaseContext { get; set; }

        public abstract void Add(T item, bool saveChanges);
        public abstract Task AddAsync(T item, bool saveChanges);
        public abstract T Find(object id);
        public abstract Task<T> FindAsync(object id);
        public abstract ICollection<T> FindAll();
        public abstract Task<List<T>> FindAllAsync();        
        public abstract void Remove(T item, bool saveChanges);
        public abstract Task RemoveAsync(T item, bool saveChanges);
        public abstract void Update(T item, bool saveChanges);
        public abstract Task UpdateAsync(T item, bool saveChanges);
        public abstract bool Exists(object id);
        public abstract Task<bool> ExistsAsync(object id);

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
