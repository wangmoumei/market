using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Market.Models;

namespace Market.DAL
{
    public class MarketContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderHead> OrderHeads { get; set; }
        public DbSet<OrderBody> OrderBodys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Banner> Banners { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}