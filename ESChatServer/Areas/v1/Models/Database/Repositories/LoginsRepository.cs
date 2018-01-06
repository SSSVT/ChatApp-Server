//using ESChatServer.Areas.v1.Models.Database.Entities;
//using ESChatServer.Areas.v1.Models.Database.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ESChatServer.Areas.v1.Models.Database.Repositories
//{
//    public class LoginsRepository : Repository<Login>, ILoginsRepository
//    {
//        public LoginsRepository(DatabaseContext context) : base(context)
//        {
//        }

//        public override void Add(Login item, bool saveChanges)
//        {
//            this._DatabaseContext.Logins.Add(item);

//            if (saveChanges)
//                this.SaveChanges();
//        }
//        public override async Task AddAsync(Login item, bool saveChanges)
//        {
//            await this._DatabaseContext.Logins.AddAsync(item);

//            if (saveChanges)
//                await this.SaveChangesAsync();
//        }

//        public override Login Find(object id)
//        {
//            return this._DatabaseContext.Logins.Find(id);
//        }
//        public override async Task<Login> FindAsync(object id)
//        {
//            return await this._DatabaseContext.Logins.FindAsync(id);
//        }

//        public override ICollection<Login> FindAll()
//        {
//            return this._DatabaseContext.Logins.ToList();
//        }
//        public override async Task<List<Login>> FindAllAsync()
//        {
//            return await this._DatabaseContext.Logins.ToListAsync();
//        }

//        public override void Remove(Login item, bool saveChanges)
//        {
//            this._DatabaseContext.Logins.Remove(item);

//            if (saveChanges)
//                this.SaveChanges();
//        }
//        public override async Task RemoveAsync(Login item, bool saveChanges)
//        {
//            this._DatabaseContext.Logins.Remove(item);

//            if (saveChanges)
//                await this.SaveChangesAsync();
//        }

//        public override void Update(Login item, bool saveChanges)
//        {
//            Login login = this.Find(item.ID);
//            login.IDUser = item.IDUser;
//            login.UTCLoginTime = item.UTCLoginTime;
//            login.UTCLogoutTime = item.UTCLogoutTime;
//            login.UserAgent = item.UserAgent;
//            login.IPAddress = item.IPAddress;

//            //TODO: Update virtual properties?
//            //login.User = item.User;

//            if (saveChanges)
//                this.SaveChanges();
//        }
//        public override async Task UpdateAsync(Login item, bool saveChanges)
//        {
//            Login login = await this.FindAsync(item.ID);
//            login.IDUser = item.IDUser;
//            login.UTCLoginTime = item.UTCLoginTime;
//            login.UTCLogoutTime = item.UTCLogoutTime;
//            login.UserAgent = item.UserAgent;
//            login.IPAddress = item.IPAddress;

//            //TODO: Update virtual properties?
//            //login.User = item.User;

//            if (saveChanges)
//                await this.SaveChangesAsync();
//        }

//        public override bool Exists(object id)
//        {
//            return this.Find(id) != null;
//        }
//        public override async Task<bool> ExistsAsync(object id)
//        {
//            return await this.FindAsync(id) != null;
//        }

//        public ICollection<Login> FindByUserID(long id)
//        {
//            return this._DatabaseContext.Logins.Where(x => x.IDUser == id).ToList();
//        }
//        public async Task<ICollection<Login>> FindByUserIDAsync(long id)
//        {
//            return await this._DatabaseContext.Logins.Where(x => x.IDUser == id).ToListAsync();
//        }
//    }
//}
