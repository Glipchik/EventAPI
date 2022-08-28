using EventAPI.Business.Interfaces;
using EventAPI.Core.Entities;
using EventAPI.DAL.Interfaces;

namespace EventAPI.Business.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync() => await _eventRepository.GetAllAsync();

        public async Task<Event?> GetEventAsync(int id) => await _eventRepository.GetByIdAsync(id);

        public async Task<int> CreateEventAsync(Event @event)
        {
            var eventId = await _eventRepository.CreateAsync(@event);

            _unitOfWork.SaveChanges();

            return eventId;
        }

        public async Task<bool> DeleteEventAsync(string name)
        {
            var result = await _eventRepository.DeleteAsync(name);

            _unitOfWork.SaveChanges();

            return result;
        }

        public async Task<bool> UpdateEventAsync(Event @event)
        {
            var eventIsUpdated = await _eventRepository.UpdateAsync(@event);

            if (eventIsUpdated)
            {
                _unitOfWork.SaveChanges();
            }

            return eventIsUpdated;
        }
    }
}
