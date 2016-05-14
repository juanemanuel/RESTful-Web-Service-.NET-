using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Events
{
    public class EventDTO : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
    }
}
