using EventAPI.Core.Entities;

namespace EventAPI.DAL.Interfaces
{
    public interface IEventRepository
    {
        public int Create(Event @event);

        public void Delete(string name);

        public IEnumerable<Event> GetAll();

        public Event GetById(int id);

        public void Update(Event @event);
    }
}
