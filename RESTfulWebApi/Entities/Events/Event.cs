using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Events
{
    [Table("Event")]
    public class Event : IEvent
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
    }
}
