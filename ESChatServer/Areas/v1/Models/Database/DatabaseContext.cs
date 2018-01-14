using ESChatServer.Areas.v1.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ESChatServer.Areas.v1.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        #region DbSets
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friendship>().ToTable("es_tbFriendships");
            modelBuilder.Entity<Message>().ToTable("es_tbMessages");
            modelBuilder.Entity<Participant>().ToTable("es_tbRoomParticipants");
            modelBuilder.Entity<PasswordReset>().ToTable("es_tbPasswordResets");
            modelBuilder.Entity<Room>().ToTable("es_tbRooms");
            modelBuilder.Entity<User>().ToTable("es_tbUsers");

            #region Friendship
            modelBuilder.Entity<Friendship>()
                .Property(x => x.ID)
                .HasColumnName("ID");
            modelBuilder.Entity<Friendship>()
                .HasKey(x => x.ID);
            modelBuilder.Entity<Friendship>()
                .Property(x => x.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Friendship>()
                .Property(x => x.IDSender)
                .HasColumnName("IDes_tbUsers_SENDER");

            modelBuilder.Entity<Friendship>()
                .Property(x => x.IDRecipient)
                .HasColumnName("IDes_tbUsers_RECIPIENT");

            modelBuilder.Entity<Friendship>()
                .Property(x => x.UTCServerReceived)
                .HasColumnName("REQUEST_SERVER_RECEIVED_UTC");

            modelBuilder.Entity<Friendship>()
                .Property(x => x.UTCAccepted)
                .HasColumnName("REQUEST_ACCEPTED_UTC");

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Sender)
                .WithMany(s => s.SentFriendships)
                .HasForeignKey(f => f.IDSender)
                .HasConstraintName("FK_es_tbFriendships_IDes_tbUsers_SENDER");

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Recipient)
                .WithMany(r => r.ReceivedFriendships)
                .HasForeignKey(f => f.IDRecipient)
                .HasConstraintName("FK_es_tbFriendships_IDes_tbUsers_RECIPIENT");
            #endregion
            #region Login
            //modelBuilder.Entity<Login>()
            //    .Property(x => x.ID)
            //    .HasColumnName("ID");
            //modelBuilder.Entity<Login>()
            //    .HasKey(x => x.ID);
            //modelBuilder.Entity<Login>()
            //    .Property(x => x.ID)
            //    .ValueGeneratedOnAdd();

            //modelBuilder.Entity<Login>()
            //    .Property(x => x.IDUser)
            //    .HasColumnName("IDes_tbUsers");

            //modelBuilder.Entity<Login>()
            //    .Property(x => x.UTCLoginTime)
            //    .HasColumnName("LOGIN_TIME_UTC");
            //modelBuilder.Entity<Login>()
            //    .Property(x => x.UTCLoginTime)
            //    .HasDefaultValue(DateTime.UtcNow);

            //modelBuilder.Entity<Login>()
            //    .Property(x => x.UTCLogoutTime)
            //    .HasColumnName("LOGOUT_TIME_UTC");

            //modelBuilder.Entity<Login>()
            //    .Property(x => x.UserAgent)
            //    .HasColumnName("USER_AGENT");

            //modelBuilder.Entity<Login>()
            //    .Property(x => x.IPAddress)
            //    .HasColumnName("USER_IP");

            //modelBuilder.Entity<Login>()
            //    .HasOne(l => l.User)
            //    .WithMany(u => u.Logins)
            //    .HasForeignKey(l => l.IDUser)
            //    .HasConstraintName("FK_es_tbLogins_IDes_tbUsers");
            #endregion
            #region Message
            modelBuilder.Entity<Message>()
                .Property(x => x.ID)
                .HasColumnName("ID");
            modelBuilder.Entity<Message>()
                .HasKey(x => x.ID);
            modelBuilder.Entity<Message>()
                .Property(x => x.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Message>()
                .Property(x => x.IDRoom)
                .HasColumnName("IDes_tbRooms");

            modelBuilder.Entity<Message>()
                .Property(x => x.IDUser)
                .HasColumnName("IDes_tbUsers");

            modelBuilder.Entity<Message>()
                .Property(x => x.UTCSend)
                .HasColumnName("SENT_UTC");

            modelBuilder.Entity<Message>()
                .Property(x => x.UTCServerReceived)
                .HasColumnName("SERVER_RECEIVED_UTC");

            modelBuilder.Entity<Message>()
                .Property(x => x.Content)
                .HasColumnName("CONTENT");

            modelBuilder.Entity<Message>()
                .Ignore(x => x.Sender);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Owner)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.IDUser)
                .HasConstraintName("FK_es_tbMessages_IDes_tbUsers");

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Room)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.IDRoom)
                .HasConstraintName("FK_es_tbMessages_IDes_tbRooms");
            #endregion
            #region Participant
            modelBuilder.Entity<Participant>()
                .Property(x => x.ID)
                .HasColumnName("ID");
            modelBuilder.Entity<Participant>()
                .HasKey(x => x.ID);
            modelBuilder.Entity<Participant>()
                .Property(x => x.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Participant>()
                .Property(x => x.IDRoom)
                .HasColumnName("IDes_tbRooms");

            modelBuilder.Entity<Participant>()
                .Property(x => x.IDUser)
                .HasColumnName("IDes_tbUsers");

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Room)
                .WithMany(r => r.Participants)
                .HasForeignKey(p => p.IDRoom)
                .HasConstraintName("FK_es_tbRoomParticipants_IDes_tbRooms");

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.User)
                .WithMany(u => u.Participants)
                .HasForeignKey(p => p.IDUser)
                .HasConstraintName("FK_es_tbRoomParticipants_IDes_tbUsers");
            #endregion
            #region PasswordReset
            modelBuilder.Entity<PasswordReset>()
                .Property(x => x.Id)
                .HasColumnName("ID");
            modelBuilder.Entity<PasswordReset>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<PasswordReset>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PasswordReset>()
                .Property(x => x.UserId)
                .HasColumnName("IDes_tbUsers");

            modelBuilder.Entity<PasswordReset>()
                .Property(x => x.UtcIssued)
                .HasColumnName("ISSUED_UTC");
            modelBuilder.Entity<PasswordReset>()
                .Property(x => x.UtcIssued)
                .HasDefaultValue(DateTime.UtcNow);

            modelBuilder.Entity<PasswordReset>()
                .Property(x => x.UtcExpiration)
                .HasColumnName("EXPIRE_UTC");
            modelBuilder.Entity<PasswordReset>()
                .Property(x => x.UtcExpiration)
                .HasDefaultValue(DateTime.UtcNow.AddMinutes(10));

            modelBuilder.Entity<PasswordReset>()
                .Property(x => x.Used)
                .HasColumnName("USED");

            modelBuilder.Entity<PasswordReset>()
                .HasOne(r => r.User)
                .WithMany(u => u.PasswordResets)
                .HasForeignKey(r => r.UserId)
                .HasConstraintName("FK_es_tbPasswordResets_IDes_tbUsers");
            #endregion
            #region Room
            modelBuilder.Entity<Room>()
                .Property(x => x.ID)
                .HasColumnName("ID");
            modelBuilder.Entity<Room>()
                .HasKey(x => x.ID);
            modelBuilder.Entity<Room>()
                .Property(x => x.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Room>()
                .Property(x => x.IDOwner)
                .HasColumnName("IDes_tbUsers");

            modelBuilder.Entity<Room>()
                .Property(x => x.Name)
                .HasColumnName("ROOM_NAME");

            modelBuilder.Entity<Room>()
                .Property(x => x.Description)
                .HasColumnName("ROOM_DESCRIPTION");

            modelBuilder.Entity<Room>()
                .Property(x => x.UTCCreationDate)
                .HasColumnName("ROOM_CREATED_UTC");

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Owner)
                .WithMany(u => u.OwnedRooms)
                .HasForeignKey(r => r.IDOwner)
                .HasConstraintName("FK_es_tbRooms_IDes_tbUsers");
            #endregion
            #region User
            modelBuilder.Entity<User>()
                .Property(x => x.ID)
                .HasColumnName("ID");
            modelBuilder.Entity<User>()
                .HasKey(x => x.ID);
            modelBuilder.Entity<User>()
                .Property(x => x.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .HasColumnName("FIRST_NAME");

            modelBuilder.Entity<User>()
                .Property(x => x.MiddleName)
                .HasColumnName("MIDDLE_NAME");

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasColumnName("LAST_NAME");

            modelBuilder.Entity<User>()
                .Property(x => x.Birthdate)
                .HasColumnName("BIRTHDAY");
            modelBuilder.Entity<User>()
                .Property(x => x.Birthdate)
                .HasColumnType("date");

            modelBuilder.Entity<User>()
                .Property(x => x.Gender)
                .HasColumnName("GENDER");
            modelBuilder.Entity<User>()
                .Property(x => x.Gender)
                .HasColumnType("char(1)");

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .HasColumnName("USERNAME");
            modelBuilder.Entity<User>()
                .Ignore(x => x.Password);

            modelBuilder.Entity<User>()
                .Property(x => x.PasswordHash)
                .HasColumnName("PSWD_HASH");

            modelBuilder.Entity<User>()
                .Property(x => x.PasswordSalt)
                .HasColumnName("PSWD_SALT");

            modelBuilder.Entity<User>()
                .Property(x => x.UTCRegistrationDate)
                .HasColumnName("REGISTERED_ON_UTC");
            modelBuilder.Entity<User>()
                .Property(x => x.UTCRegistrationDate)
                .HasDefaultValue(DateTime.UtcNow);
            
            modelBuilder.Entity<User>()
                .Property(x => x.Status)
                .HasColumnName("USER_STATUS");
            #endregion
        }
    }
}
