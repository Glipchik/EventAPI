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

        /// <summary>
        /// Gets the list of all events
        /// </summary>
        /// <remarks>Type of the returned list: IEnumerable of Event</remarks>
        /// <returns>Returns a list of events</returns>
        /// <response code="200">Success</response>
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEventsAsync() =>
            Ok(await _eventService.GetAllEventsAsync());

        /// <summary>
        /// Gets the event by id
        /// </summary>
        /// <param name="id">Event id</param>
        /// <returns>Returns found event or NotFound</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If event is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Event>> GetEventAsync(int id)
        {
            var @event = await _eventService.GetEventAsync(id);

            if (@event is null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        /// <summary>
        /// Creates the event
        /// </summary>
        /// <param name="createModel">Event to create</param>
        /// <returns>Returns id of the created event</returns>
        /// <response code="200">Success</response>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> CreateEventAsync(CreateEventModel createModel)
        {
            var id = await _eventService.CreateEventAsync(_mapper.Map<Event>(createModel));

            return Ok(id);
        }

        /// <summary>
        /// Deletes the event by the name
        /// </summary>
        /// <param name="name">Name of the event that should be deleted</param>
        /// <returns>Returns Ok or not found</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If event is not found</response>
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteEventAsync(string name)
        {
            var eventIsDeleted = await _eventService.DeleteEventAsync(name);

            if (eventIsDeleted)
            {
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Updates the event by the id
        /// </summary>
        /// <param name="updateModel">Event that should be updated</param>
        /// <returns>Returns Ok or not found</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If event is not found</response>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateEventAsync(UpdateEventModel updateModel)
        {
            var eventIsFound = await _eventService.UpdateEventAsync(_mapper.Map<Event>(updateModel));

            if (eventIsFound)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
