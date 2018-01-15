using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Repositories
{
    public class RoomsRepository : Repository<Room>, IRoomsRepository
    {
        public RoomsRepository(DatabaseContext context) : base(context)
        {
        }

        public void Add(Room item, bool saveChanges)
        {
            this._DatabaseContext.Rooms.Add(item);

            if (saveChanges)
                this.SaveChanges();
        }
        public async Task AddAsync(Room item, bool saveChanges)
        {
            await this._DatabaseContext.Rooms.AddAsync(item);

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public Room Find(object id)
        {
            return this._DatabaseContext.Rooms.Find(id);
        }
        public async Task<Room> FindAsync(object id)
        {
            return await this._DatabaseContext.Rooms.FindAsync(id);
        }

        public ICollection<Room> FindAll()
        {
            return this._DatabaseContext.Rooms.ToList();
        }
        public async Task<List<Room>> FindAllAsync()
        {
            return await this._DatabaseContext.Rooms.ToListAsync();
        }

        public void Remove(Room item, bool saveChanges)
        {
            IParticipantsRepository participantsRepository = new ParticipantsRepository(this._DatabaseContext);

            ICollection<Participant> participants = participantsRepository.FindByRoomID(item.ID);
            foreach (var entity in participants)
            {
                participantsRepository.Remove(entity, false);
            }

            this._DatabaseContext.Rooms.Remove(item);

            if (saveChanges)
                this.SaveChanges();
        }
        public async Task RemoveAsync(Room item, bool saveChanges)
        {
            IParticipantsRepository participantsRepository = new ParticipantsRepository(this._DatabaseContext);

            ICollection<Participant> participants = await participantsRepository.FindByRoomIDAsync(item.ID);
            foreach (var entity in participants)
            {
                await participantsRepository.RemoveAsync(entity, false);
            }

            this._DatabaseContext.Rooms.Remove(item);

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public void Update(Room item, bool saveChanges)
        {
            Room room = this.Find(item.ID);
            room.IDOwner = item.IDOwner;
            room.Name = item.Name;
            room.Description = item.Description;

            //TODO: Update virtual properties?
            //room.Owner = item.Owner;
            //room.Participants = item.Participants;
            //room.Messages = item.Messages;

            if (saveChanges)
                this.SaveChanges();
        }
        public async Task UpdateAsync(Room item, bool saveChanges)
        {
            Room room = await this.FindAsync(item.ID);
            room.IDOwner = item.IDOwner;
            room.Name = item.Name;
            room.Description = item.Description;

            //TODO: Update virtual properties?
            //room.Owner = item.Owner;
            //room.Participants = item.Participants;
            //room.Messages = item.Messages;

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public ICollection<Room> FindByUserID(long id)
        {
            ICollection<Room> result = (from p in this._DatabaseContext.Participants
                          join r in this._DatabaseContext.Rooms on p.IDRoom equals r.ID
                          join u in this._DatabaseContext.Users on p.IDUser equals u.ID
                          where u.ID == id
                          select r
                          ).ToList();
            return result;
        }
        public async Task<ICollection<Room>> FindByUserIDAsync(long id)
        {
            ICollection<Room> result = await (from p in this._DatabaseContext.Participants
                                        join r in this._DatabaseContext.Rooms on p.IDRoom equals r.ID
                                        join u in this._DatabaseContext.Users on p.IDUser equals u.ID
                                        where u.ID == id
                                        select r
                          ).ToListAsync();
            return result;
        }

        public bool Exists(object id)
        {
            return this.Find(id) != null;
        }
        public async Task<bool> ExistsAsync(object id)
        {
            return await this.FindAsync(id) != null;
        }
    }
}
