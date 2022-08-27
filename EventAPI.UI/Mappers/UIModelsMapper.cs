using AutoMapper;
using EventAPI.Core.Entities;
using EventAPI.UI.Models;

namespace EventAPI.UI.Mappers
{
    public class UIModelsMapper : Profile
    {
        public UIModelsMapper()
        {
            CreateMap<Event, UpdateEventModel>().ReverseMap();
            CreateMap<Event, CreateEventModel>().ReverseMap();
        }
    }
}
