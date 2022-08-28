using EventAPI.Core.Entities;

namespace EventAPI.Business.Interfaces
{
    public interface IEventService
    {
        public Task<IEnumerable<Event>> GetAllEventsAsync();

        public Task<Event?> GetEventAsync(int id);

        public Task<int> CreateEventAsync(Event @event);

        public Task<bool> DeleteEventAsync(string name);

        public Task<bool> UpdateEventAsync(Event @event);
    }
}
