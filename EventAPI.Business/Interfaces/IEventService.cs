using EventAPI.Core.Entities;

namespace EventAPI.Business.Interfaces
{
    public interface IEventService
    {
        public IEnumerable<Event> GetAllEvents();

        public Event GetEvent(int id);

        public void CreateEvent(Event @event);

        public void DeleteEvent(string name);

        public void UpdateEvent(Event @event);
    }
}
