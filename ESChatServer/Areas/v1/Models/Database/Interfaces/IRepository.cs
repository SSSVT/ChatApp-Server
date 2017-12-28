using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface IRepository<T>
    {
        T Find(object id);
        Task<T> FindAsync(object id);

        ICollection<T> FindAll();
        Task<List<T>> FindAllAsync();

        void Add(T item, bool saveChanges);
        Task AddAsync(T item, bool saveChanges);

        void Remove(T item, bool saveChanges);
        Task RemoveAsync(T item, bool saveChanges);

        void Update(T item, bool saveChanges);
        Task UpdateAsync(T item, bool saveChanges);

        bool Exists(object id);
        Task<bool> ExistsAsync(object id);
    }
}
