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

        public IEnumerable<Event> GetAllEvents()
        {
            return _eventRepository.GetAll();
        }

        public Event GetEvent(int id)
        {
            var @event = _eventRepository.GetById(id);

            if (@event is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return @event;
        }

        public void CreateEvent(Event @event)
        {
            _eventRepository.Create(@event);

            _unitOfWork.SaveChanges();
        }

        public void DeleteEvent(string name)
        {
            _eventRepository.Delete(name);

            _unitOfWork.SaveChanges();
        }

        public void UpdateEvent(Event @event)
        {
            _eventRepository.Update(@event);

            _unitOfWork.SaveChanges();
        }
    }
}
