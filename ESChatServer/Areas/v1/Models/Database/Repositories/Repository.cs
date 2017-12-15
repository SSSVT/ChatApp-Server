using System.Collections.Generic;

namespace ESChatServer.Areas.v1.Models.Database.Repositories
{
    public abstract class Repository<T>
    {
        public Repository(DatabaseContext context)
        {
            this._DatabaseContext = context;
        }

        protected DatabaseContext _DatabaseContext { get; set; }
        internal virtual void SaveChanges()
        {
            this._DatabaseContext.SaveChanges();
        }

        internal abstract T Find(object id);
        internal abstract ICollection<T> FindAll();
        protected abstract void Add(T item, bool saveChanges);
        internal abstract void Remove(T item, bool saveChanges);
        internal abstract void Update(T item, bool saveChanges);
    }
}
