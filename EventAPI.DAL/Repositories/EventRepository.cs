using EventAPI.Core.Entities;
using EventAPI.Core.Exceptions;
using EventAPI.DAL.Context;
using EventAPI.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Event @event)
        {
            await _context.AddAsync(@event);

            return @event.Id;
        }

        public async Task<bool> DeleteAsync(string name)
        {
            if (_context.Events is null) throw new EventDbSetNullException();

            var @event = await _context.Events.FirstOrDefaultAsync(n => n.Name.Equals(name));

            if (@event is not null)
            {
                _context.Remove(@event);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            if (_context.Events is null) throw new EventDbSetNullException();

            return await _context.Events.ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            if (_context.Events is null) throw new EventDbSetNullException();

            return await _context.Events.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<bool> UpdateAsync(Event @event)
        {
            if (_context.Events is null) throw new EventDbSetNullException();

            var eventToUpdate = await _context.Events.FirstOrDefaultAsync(n => n.Id == @event.Id);

            if (eventToUpdate is not null)
            {
                eventToUpdate.Name = @event.Name;
                eventToUpdate.Description = @event.Description;
                eventToUpdate.Plan = @event.Plan;
                eventToUpdate.Organizer = @event.Organizer;
                eventToUpdate.Speaker = @event.Speaker;
                eventToUpdate.EventTime = @event.EventTime;
                eventToUpdate.EventPlace = @event.EventPlace;

                return true;
            }

            return false;
        }
    }
}
