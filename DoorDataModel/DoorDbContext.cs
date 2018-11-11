using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoorDataModel
{
    public class DoorDbContext : DbContext
    {
        public DbSet<PARAM> PARAMS { get; set; }
    }
}