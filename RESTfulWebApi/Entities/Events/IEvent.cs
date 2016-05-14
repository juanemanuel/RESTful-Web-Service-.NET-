using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Events
{
    public interface IEvent
    {
        Guid Id { get; set; }
        string Name { get; set; }
        DateTime? StartingDate { get; set; }
        DateTime? EndingDate { get; set; }
        string Description { get; set; }
        string Creator { get; set; }
    }
}
