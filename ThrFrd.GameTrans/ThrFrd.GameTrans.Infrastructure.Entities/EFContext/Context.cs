﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Text.RegularExpressions;
using ThrFrd.GameTrans.Infrastructure.Entities.Repository;

namespace ThrFrd.GameTrans.Infrastructure.Entities.EFContext
{
    public class Context : DbContext
    {
        public Context() : this("name=Context") { }
        public Context(string contextName)
            : base(contextName)
        {
            Database.SetInitializer<Context>(null);
        }
        public DbSet<T_User> User { get; set; }
        public DbSet<T_Module> Module { get; set; }
        public DbSet<T_UserModule> UserModule { get; set; }
        public DbSet<T_CheckCodeStream> CheckCodeStream { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//移除复数表名的契约
            modelBuilder.Properties<decimal>().Configure(config => config.HasPrecision(18, 4));
        }
    }
}