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

        public void Add(Friendship item, bool saveChanges)
        {
            this._DatabaseContext.Friendships.Add(item);

            if (saveChanges)
                this.SaveChanges();
        }
        public async Task AddAsync(Friendship item, bool saveChanges)
        {
            await this._DatabaseContext.Friendships.AddAsync(item);

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public Friendship Find(object id)
        {
            return this._DatabaseContext.Friendships.Find(id);
        }
        public async Task<Friendship> FindAsync(object id)
        {
            return await this._DatabaseContext.Friendships.FindAsync(id);
        }

        public ICollection<Friendship> FindAll()
        {
            return this._DatabaseContext.Friendships.ToList();
        }
        public async Task<List<Friendship>> FindAllAsync()
        {
            return await this._DatabaseContext.Friendships.ToListAsync();
        }

        public void Remove(Friendship item, bool saveChanges)
        {
            this._DatabaseContext.Friendships.Remove(item);

            if (saveChanges)
                this.SaveChanges();
        }
        public async Task RemoveAsync(Friendship item, bool saveChanges)
        {
            this._DatabaseContext.Friendships.Remove(item);

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public void Update(Friendship item, bool saveChanges)
        {
            Friendship friendship = this.Find(item.ID);
            friendship.IDSender = item.IDSender;
            friendship.IDRecipient = item.IDRecipient;
            friendship.UTCServerReceived = item.UTCServerReceived;
            friendship.UTCAccepted = item.UTCAccepted;

            //TODO: Update virtual properties?
            //friendship.Sender = item.Sender;
            //friendship.Recipient = item.Recipient;

            if (saveChanges)
                this.SaveChanges();
        }
        public async Task UpdateAsync(Friendship item, bool saveChanges)
        {
            Friendship friendship = await this.FindAsync(item.ID);
            friendship.IDSender = item.IDSender;
            friendship.IDRecipient = item.IDRecipient;
            friendship.UTCServerReceived = item.UTCServerReceived;
            friendship.UTCAccepted = item.UTCAccepted;

            //TODO: Update virtual properties?
            //friendship.Sender = item.Sender;
            //friendship.Recipient = item.Recipient;

            if (saveChanges)
                await this.SaveChangesAsync();
        }

        public bool Exists(object id)
        {
            return this.Find(id) != null;
        }
        public async Task<bool> ExistsAsync(object id)
        {
            return await this.FindAsync(id) != null;
        }

        public ICollection<Friendship> FindReceivedAndPendingByUserID(long id)
        {
            return this._DatabaseContext.Friendships.Where(x => x.IDRecipient == id && x.UTCAccepted == null).Include(x => x.Sender).ToList();
        }
        public async Task<ICollection<Friendship>> FindReceivedAndPendingByUserIDAsync(long id)
        {
            return await this._DatabaseContext.Friendships.Where(x => x.IDRecipient == id && x.UTCAccepted == null).Include(x => x.Sender).ToListAsync();
        }

        public ICollection<Friendship> FindAcceptedByUserID(long id)
        {
            return this._DatabaseContext.Friendships.Where(x => (x.IDSender == id || x.IDRecipient == id) && x.UTCAccepted != null).Include(x => x.Sender).Include(x => x.Recipient).ToList();
        }
        public async Task<ICollection<Friendship>> FindAcceptedByUserIDAsync(long id)
        {
            return await this._DatabaseContext.Friendships.Where(x => (x.IDSender == id || x.IDRecipient == id) && x.UTCAccepted != null).Include(x => x.Sender).Include(x => x.Recipient).ToListAsync();
        }

        public ICollection<Friendship> FindByUserID(long id)
        {
            return this._DatabaseContext.Friendships.Where(x => x.IDSender == id || x.IDRecipient == id).Include(x => x.Sender).Include(x => x.Recipient).ToList();
        }
        public async Task<ICollection<Friendship>> FindByUserIDAsync(long id)
        {
            return await this._DatabaseContext.Friendships.Where(x => x.IDSender == id || x.IDRecipient == id).Include(x => x.Sender).Include(x => x.Recipient).ToListAsync();
        }

        public bool IsFriend(long currentUserID, long otherUserID)
        {
            Friendship friendship = this._DatabaseContext.Friendships.Where(
                x =>
                (x.IDSender == currentUserID && x.IDRecipient == otherUserID) ||
                (x.IDSender == otherUserID && x.IDRecipient == currentUserID)
                ).FirstOrDefault();

            return friendship != null;
        }
        public async Task<bool> IsFriendAsync(long currentUserID, long otherUserID)
        {
            Friendship friendship = await this._DatabaseContext.Friendships.Where(
                x =>
                (x.IDSender == currentUserID && x.IDRecipient == otherUserID) ||
                (x.IDSender == otherUserID && x.IDRecipient == currentUserID)
                ).FirstOrDefaultAsync();

            return friendship != null;
        }
    }
}
