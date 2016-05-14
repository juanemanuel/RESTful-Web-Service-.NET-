using Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendWebApi.Helpers
{
    public class EventFactory
    {
        public static TClass CreateInstance<TClass>(IEvent evt) where TClass : IEvent
        {
            var eventShape = Activator.CreateInstance<TClass>();

            eventShape.Id = evt.Id;
            eventShape.Creator = evt.Creator;
            eventShape.Description = evt.Description;
            eventShape.EndingDate = evt.EndingDate;
            eventShape.Name = evt.Name;
            eventShape.StartingDate = evt.StartingDate;

            return eventShape;
        }
    }
}