using EventAPI.Core.Entities;
using EventAPI.DAL.Context;
using EventAPI.DAL.Interfaces;

namespace EventAPI.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Create(Event @event)
        {
            _context.Add(@event);

            return @event.Id;
        }

        public void Delete(string name)
        {
            var @event = _context.Events.FirstOrDefault(n => n.Name.Equals(name));

            if (@event is not null) _context.Remove(@event);
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events;
        }

        public Event GetById(int id)
        {
            return _context.Events.FirstOrDefault(n => n.Id == id);
        }

        public void Update(Event @event)
        {
            Delete(@event.Name);

            Create(@event);
        }
    }
}
