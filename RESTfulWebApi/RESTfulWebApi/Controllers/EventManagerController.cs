using Entities.Events;
using Newtonsoft.Json;
using RESTfulWebApi.Helpers;
using RESTfulWebApi.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RESTfulWebApi.Controllers
{
    public class EventManagerController : Controller
    {
        // GET: Events
        public ActionResult Events()
        {
            var events = GetEvents();
            ViewBag.Events = events;
            ViewBag.HasEvents = events.Count() > 0;
            return View("Events");
        }

        public ActionResult Create()
        {
            return View("Edit", new EventModel() { IsCreation = true });
        }

        public ActionResult Edit(Guid id)
        {
            var evt = GetEvent(id);
            ((EventModel)evt).IsCreation = false;
            return View(evt);
        }

        public ActionResult Info(Guid id)
        {
            var evt = GetEvent(id);
            ViewBag.Event = evt;
            return View("Event");
        }

        private object GetEvent(Guid value)
        {
            var client = new RequestBuilder("Events");
            return ConvertToEventModel(client.GetObject<EventDTO>(value));
        }

        public ActionResult DeleteEvent(EventModel evt)
        {
            var client = new RequestBuilder("Events");
            client.DeleteObject(evt.Id);

            return RedirectToAction("Events");
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEvent(EventModel model)
        {
            var evtDTO = ConvertToEventDTO(model);
            var client = new RequestBuilder("Events");
            EventDTO result = null;
            EventModel newModel = null;

            if (model.IsCreation)
            {
                result = client.PostObject<EventDTO>(evtDTO);
            }
            else
            {
                result = client.PutObject<EventDTO>(evtDTO, model.Id);
            }

            newModel = ConvertToEventModel(result);
            newModel.IsCreation = false;

            return RedirectToAction("Edit", newModel.Id);
        }

        private IEnumerable<EventModel> GetEvents()
        {
            var client = new RequestBuilder("Events");

            return client.GetObject<List<EventDTO>>().Select(x => ConvertToEventModel(x));
        }

        private EventModel ConvertToEventModel(IEvent evt)
        {
            return new EventModel()
            {
                Id = evt.Id,
                Creator = evt.Creator,
                Description = evt.Description,
                EndingDate = evt.EndingDate,
                Name = evt.Name,
                StartingDate = evt.StartingDate,
            };
        }

        private EventDTO ConvertToEventDTO(EventModel evt)
        {
            return new EventDTO()
            {
                Id = evt.Id,
                Creator = evt.Creator,
                Description = evt.Description,
                EndingDate = evt.EndingDate,
                Name = evt.Name,
                StartingDate = evt.StartingDate,
            };
        }
    }
}