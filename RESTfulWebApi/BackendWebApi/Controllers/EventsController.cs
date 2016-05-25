using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BackendWebApi.Contexts;
using Entities.Events;
using BackendWebApi.Helpers;
using System.Web.Http.OData;

namespace BackendWebApi.Controllers
{
    public class EventsController : ApiController
    {
        private EventContext db = new EventContext();

        //Habilitando OData para filtra através de query string
        [EnableQuery]
        [HttpGet]
        [Route("Events/")]
        public IQueryable<IEvent> GetEvents()
        {
            return db.Events.AsQueryable<IEvent>();
        }

        [HttpGet]
        [Route("Events/{id}")]
        public IHttpActionResult GetEvent(Guid id)
        {
            Event @event = db.Events.Find(id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(EventFactory.CreateInstance<EventDTO>(@event));
        }

        [HttpPut]
        [Route("Events/{id}")]
        public IHttpActionResult PutEvent(Guid id, [FromBody]EventDTO @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!EventExists(@event.Id) || id != @event.Id)
            {
                return NotFound();
            }

            var evt = GetEventFromDB(@event.Id);
            var updatedEvt = TransferData(@event, evt);

            db.Entry(updatedEvt).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(@event.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(EventFactory.CreateInstance<EventDTO>(updatedEvt));
        }

        [HttpPost]
        [Route("Events/")]
        public IHttpActionResult PostEvent([FromBody]EventDTO @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newEvt = EventFactory.CreateInstance<Event>(@event);
            newEvt.Id = Guid.NewGuid();
            db.Events.Add(newEvt);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EventExists(@event.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(EventFactory.CreateInstance<EventDTO>(newEvt));
        }

        [HttpDelete]
        [Route("Events/{id}")]
        public IHttpActionResult DeleteEvent(Guid id)
        {
            bool result = RemoveEventFromDB(id);

            if (result == false)
            {
                return NotFound();
            }

            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(Guid id)
        {
            return db.Events.Count(e => e.Id == id) > 0;
        }

        private IEvent GetEventFromDB(Guid id)
        {
            return db.Events.Find(id);
        }

        private bool RemoveEventFromDB(Guid id)
        {
            var removed = true;

            if (EventExists(id))
            {
                var evt = GetEventFromDB(id);
                db.Events.Remove((Event)evt);
            }
            else
            {
                removed = false;
            }

            return removed;
        }

        private IEvent TransferData(IEvent newEvt, IEvent oldEvent)
        {
            oldEvent.Creator = newEvt.Creator;
            oldEvent.Description = newEvt.Description;
            oldEvent.EndingDate = newEvt.EndingDate;
            oldEvent.Name = newEvt.Name;
            oldEvent.StartingDate = newEvt.StartingDate;

            return oldEvent;
        }
    }
}