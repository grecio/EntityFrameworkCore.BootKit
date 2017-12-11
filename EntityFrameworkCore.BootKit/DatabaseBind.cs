﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace EntityFrameworkCore.BootKit
{
    public class DatabaseBind
    {
        public DbConnection MasterConnection { get; set; }
        public DataContext DbContextMaster { get; set; }

        public List<DbConnection> SlaveConnections { get; set; }
        public List<DataContext> DbContextSlavers { get; set; }

        public Type TableInterface { get; set; }

        public Type DbContextType { get; set; }

        public List<Type> Entities { get; set; }

        public Boolean CreateDbIfNotExist { get; set; }

        public String[] AssemblyNames { get; set; }
    }
}
