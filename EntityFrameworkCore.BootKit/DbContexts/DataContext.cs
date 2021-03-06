﻿using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace EntityFrameworkCore.BootKit
{
    public class DataContext : DbContextWithTriggers
    {
        public String ConnectionString = "";
        public List<Type> EntityTypes { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        
        public DataContext(DbContextOptions options, IServiceProvider serviceProvider)
            : base(serviceProvider, options)
        {
            ServiceProvider = serviceProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // http://www.learnentityframeworkcore.com/
            // don't need this code.
            //modelBuilder.Entity<Bundle>().ForSqlServerToTable("Bundles");
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            EntityTypes.ForEach(type =>
            {
                var type1 = modelBuilder.Model.FindEntityType(type);
                if(type1 == null)
                {
                    modelBuilder.Model.AddEntityType(type);
                }
            });

            base.OnModelCreating(modelBuilder);
        }

        protected void SetLog(DbContextOptionsBuilder optionsBuilder)
        {
            var dbSettings = (DatabaseSettings)ServiceProvider.GetService(typeof(DatabaseSettings));
            if (dbSettings.EnableSqlLog)
                optionsBuilder.UseLoggerFactory((ILoggerFactory)ServiceProvider.GetService(typeof(ILoggerFactory)));
            if (dbSettings.EnableSensitiveDataLogging)
                optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
