﻿using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database.Repositories
{
    public class FriendshipsRepository : Repository<Friendship>, IFriendshipsRepository
    {
        public FriendshipsRepository(DatabaseContext context) : base(context)
        {
        }

        public override void Add(Friendship item, bool saveChanges)
        {
            this._DatabaseContext.Friendships.Add(item);

            if (saveChanges)
                this.SaveChanges();
        }
        public override async Task AddAsync(Friendship item, bool saveChanges)
        {
            await this._DatabaseContext.Friendships.AddAsync(item);

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public override Friendship Find(object id)
        {
            return this._DatabaseContext.Friendships.Find(id);
        }
        public override async Task<Friendship> FindAsync(object id)
        {
            return await this._DatabaseContext.Friendships.FindAsync(id);
        }

        public override ICollection<Friendship> FindAll()
        {
            return this._DatabaseContext.Friendships.ToList();
        }
        public override async Task<List<Friendship>> FindAllAsync()
        {
            return await this._DatabaseContext.Friendships.ToListAsync();
        }

        public override void Remove(Friendship item, bool saveChanges)
        {
            this._DatabaseContext.Friendships.Remove(item);

            if (saveChanges)
                this.SaveChanges();
        }
        public override async Task RemoveAsync(Friendship item, bool saveChanges)
        {
            this._DatabaseContext.Friendships.Remove(item);

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public override void Update(Friendship item, bool saveChanges)
        {
            Friendship friendship = this.Find(item.ID);
            friendship.IDSender = item.IDSender;
            friendship.IDRecipient = item.IDRecipient;
            friendship.UTCSend = item.UTCSend;
            friendship.UTCAccepted = item.UTCAccepted;

            //TODO: Update virtual properties?
            //friendship.Sender = item.Sender;
            //friendship.Recipient = item.Recipient;

            if (saveChanges)
                this.SaveChanges();
        }
        public override async Task UpdateAsync(Friendship item, bool saveChanges)
        {
            Friendship friendship = await this.FindAsync(item.ID);
            friendship.IDSender = item.IDSender;
            friendship.IDRecipient = item.IDRecipient;
            friendship.UTCSend = item.UTCSend;
            friendship.UTCAccepted = item.UTCAccepted;

            //TODO: Update virtual properties?
            //friendship.Sender = item.Sender;
            //friendship.Recipient = item.Recipient;

            if (saveChanges)
                await this.SaveChangesAsync();
        }
    }
}