using System.Collections.Generic;
using System.Threading.Tasks;
using ESChatServer.Areas.v1.Models.Database.Entities;

namespace ESChatServer.Areas.v1.Models.Database.Interfaces
{
    public interface IRoomsRepository
    {
        void Add(Room item, bool saveChanges);
        Task AddAsync(Room item, bool saveChanges);
        Room Find(object id);
        ICollection<Room> FindAll();
        Task<List<Room>> FindAllAsync();
        Task<Room> FindAsync(object id);
        void Remove(Room item, bool saveChanges);
        Task RemoveAsync(Room item, bool saveChanges);
        void Update(Room item, bool saveChanges);
        Task UpdateAsync(Room item, bool saveChanges);
    }
}