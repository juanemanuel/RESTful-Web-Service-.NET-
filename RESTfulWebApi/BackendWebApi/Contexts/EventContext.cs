using Entities.Events;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackendWebApi.Contexts
{
    public class EventContext : DbContext
    {
        public EventContext()
        {
            this.Database.CommandTimeout = 120;
        }
        public DbSet<Event> Events { get; set; }
    }
}