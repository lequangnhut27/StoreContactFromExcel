using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StoreContactFromExcel.Data.DbContexts
{
    public class InfoDbContextFactory : IDesignTimeDbContextFactory<InfoDbContext>
    {
        public InfoDbContext CreateDbContext(string[] args)
        {
            string connectionString = "Server=.;Database=InfoDb;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<InfoDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new InfoDbContext(optionsBuilder.Options);
        }
    }
}
