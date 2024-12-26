﻿using System;
using System.Collections.Generic;
using MeetingPlanner.Dto;
using MeetingPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _service;
        
        public EventsController(IEventService service)
        {
            _service = service;
        }

        // GET: api/events
        [Authorize]
        [HttpGet]
        public IEnumerable<EventResponse> GetAll([FromQuery(Name = "date")] DateTime date)
        {
            return _service.GetAllPersonal(date, HttpContext.User);
        }

        // GET: api/events/global
        [HttpGet("global")]
        public IEnumerable<EventResponse> GetAllGlobal([FromQuery(Name = "date")] DateTime date)
        {
            return _service.GetAllGlobal(date);
        }

        // GET: api/events/{id}
        [Authorize]
        [HttpGet("{id}")]
        public EventResponse Get(string id)
        {
            return _service.GetOneById(id, false, HttpContext.User);
        }

        // GET: api/events/global/{id}
        [HttpGet("global/{id}")]
        public EventResponse GetGlobal(string id)
        {
            return _service.GetOneById(id, true, null);
        }

        // POST: api/events
        [HttpPost]
        public EventResponse Post([FromBody] EventRequest request)
        {
            return _service.Create(request, HttpContext.User);
        }

        // PUT: api/events/{id}
        [HttpPut("{id}")]
        public EventResponse Put(string id, [FromBody] EventRequest request)
        {
            return _service.Update(id, request, HttpContext.User);
        }

        // DELETE: api/events/{id}
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _service.Delete(id);
        }
    }
}
