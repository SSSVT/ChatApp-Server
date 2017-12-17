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
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().ToTable("es_tbLogins");
            modelBuilder.Entity<User>().ToTable("es_tbUsers");

            #region Login
            modelBuilder.Entity<Login>()
                .Property(x => x.ID)
                .HasColumnName("ID");
            modelBuilder.Entity<Login>()
                .HasKey(x => x.ID);

            modelBuilder.Entity<Login>()
                .Property(x => x.IDUser)
                .HasColumnName("IDes_tbUsers");

            modelBuilder.Entity<Login>()
                .Property(x => x.UTCLoginTime)
                .HasColumnName("LOGIN_TIME_UTC");
            modelBuilder.Entity<Login>()
                .Property(x => x.UTCLoginTime)
                .HasDefaultValue(DateTime.UtcNow);

            modelBuilder.Entity<Login>()
                .Property(x => x.UTCLogoutTime)
                .HasColumnName("LOGOUT_TIME_UTC");

            modelBuilder.Entity<Login>()
                .Property(x => x.UserAgent)
                .HasColumnName("USER_AGENT");

            modelBuilder.Entity<Login>()
                .Property(x => x.IPAddress)
                .HasColumnName("USER_IP");

            modelBuilder.Entity<Login>()
                .HasOne(l => l.User)
                .WithMany(u => u.Logins)
                .HasForeignKey(l => l.IDUser)
                .HasConstraintName("FK_es_tbLogins_IDes_tbUsers");
            #endregion
            #region User
            modelBuilder.Entity<User>()
                .Property(x => x.ID)
                .HasColumnName("ID");
            modelBuilder.Entity<User>()
                .HasKey(x => x.ID);

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
                .Property(x => x.Birthday)
                .HasColumnName("BIRTHDAY");
            modelBuilder.Entity<User>()
                .Property(x => x.Birthday)
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
            #endregion
        }
    }
}
