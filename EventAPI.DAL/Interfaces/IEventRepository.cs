using EventAPI.Core.Entities;

namespace EventAPI.DAL.Interfaces
{
    public interface IEventRepository
    {
        public Task<int> CreateAsync(Event @event);

        public Task<bool> DeleteAsync(string name);

        public Task<IEnumerable<Event>> GetAllAsync();

        public Task<Event?> GetByIdAsync(int id);

        public Task<bool> UpdateAsync(Event @event);
    }
}
