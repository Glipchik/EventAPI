using AutoMapper;
using EventAPI.Business.Interfaces;
using EventAPI.Core.Entities;
using EventAPI.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;

        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public ActionResult<IEnumerable<Event>> GetAllEvents()
        {
            var events = _eventService.GetAllEvents();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEvent(int id)
        {
            try
            {
                var @event = _eventService.GetEvent(id);

                return Ok(@event);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult CreateEvent(CreateEventModel createModel)
        {
            _eventService.CreateEvent(_mapper.Map<Event>(createModel));

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteEvent(string name)
        {
            _eventService.DeleteEvent(name);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateEvent(UpdateEventModel updateModel)
        {
            _eventService.UpdateEvent(_mapper.Map<Event>(updateModel));

            return Ok();
        }
    }
}
