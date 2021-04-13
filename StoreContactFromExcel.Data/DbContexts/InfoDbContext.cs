using Microsoft.EntityFrameworkCore;
using StoreContactFromExcel.Data.Configurations;
using StoreContactFromExcel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreContactFromExcel.Data.DbContexts
{
    public class InfoDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public InfoDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
