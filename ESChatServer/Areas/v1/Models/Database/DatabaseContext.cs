using ESChatServer.Areas.v1.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Login
            modelBuilder.Entity<Login>().ToTable("es_tbLogins");
            #endregion

            #region User
            modelBuilder.Entity<User>().ToTable("es_tbUsers");

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
                .Property(x => x.FirstName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.MiddleName)
                .HasColumnName("MIDDLE_NAME");

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasColumnName("LAST_NAME");
            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .IsRequired();


            #endregion
        }
    }
}
