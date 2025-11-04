using System;
using System.Collections.Generic;
using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lotus_Dashboard1.Apis.GoldEtemadContext
{
    public partial class MostOnlineContext : DbContext
    {


        public MostOnlineContext(DbContextOptions<MostOnlineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MostOnlineOrdersModel> mostonline { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=192.168.1.131;Initial Catalog=LotusibBI;User ID=balavand;Password=123456;Encrypt=False;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
        .Entity<MostOnlineOrdersModel>(
            eb =>
            {
                eb.HasNoKey();


            });
        }


    }
}
