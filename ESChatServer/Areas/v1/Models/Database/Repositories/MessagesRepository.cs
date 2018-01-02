using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Repositories
{
    public class MessagesRepository : Repository<Message>, IMessagesRepository
    {
        public MessagesRepository(DatabaseContext context) : base(context)
        {
        }

        public override void Add(Message item, bool saveChanges)
        {
            this._DatabaseContext.Messages.Add(item);

            if (saveChanges)
                this.SaveChanges();
        }
        public override async Task AddAsync(Message item, bool saveChanges)
        {
            await this._DatabaseContext.Messages.AddAsync(item);

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public override Message Find(object id)
        {
            return this._DatabaseContext.Messages.Find(id);
        }
        public override async Task<Message> FindAsync(object id)
        {
            return await this._DatabaseContext.Messages.FindAsync(id);
        }

        public override ICollection<Message> FindAll()
        {
            return this._DatabaseContext.Messages.ToList();
        }
        public override async Task<List<Message>> FindAllAsync()
        {
            return await this._DatabaseContext.Messages.ToListAsync();
        }

        public override void Remove(Message item, bool saveChanges)
        {
            this._DatabaseContext.Messages.Remove(item);

            if (saveChanges)
                this.SaveChanges();
        }
        public override async Task RemoveAsync(Message item, bool saveChanges)
        {
            this._DatabaseContext.Messages.Remove(item);

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public override void Update(Message item, bool saveChanges)
        {
            Message message = this.Find(item.ID);
            message.IDRoom = item.IDRoom;
            message.IDUser = item.IDUser;
            message.UTCSend = item.UTCSend;
            message.Content = item.Content;

            //TODO: Update virtual properties?
            //message.Room = item.Room;
            //message.User = item.User;

            if (saveChanges)
                this.SaveChanges();
        }
        public override async Task UpdateAsync(Message item, bool saveChanges)
        {
            Message message = await this.FindAsync(item.ID);
            message.IDRoom = item.IDRoom;
            message.IDUser = item.IDUser;
            message.UTCSend = item.UTCSend;
            message.Content = item.Content;

            //TODO: Update virtual properties?
            //message.Room = item.Room;
            //message.User = item.User;

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public ICollection<Message> FindByUserID(long id)
        {
            return this._DatabaseContext.Messages.Where(x => x.IDUser == id).ToList();
        }
        public async Task<ICollection<Message>> FindByUserIDAsync(long id)
        {
            return await this._DatabaseContext.Messages.Where(x => x.IDUser == id).ToListAsync();
        }

        public ICollection<Message> FindByRoomID(long id, DateTime time)
        {
            return this._DatabaseContext.Messages.Where(x => x.IDRoom == id && x.UTCServerReceived > time).ToList();
        }
        public async Task<ICollection<Message>> FindByRoomIDAsync(long id, DateTime time)
        {
            return await this._DatabaseContext.Messages.Where(x => x.IDRoom == id && x.UTCServerReceived > time).ToListAsync();
        }

        public override bool Exists(object id)
        {
            return this.Find(id) != null;
        }
        public override async Task<bool> ExistsAsync(object id)
        {
            return await this.FindAsync(id) != null;
        }
    }
}
