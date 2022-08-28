using AutoMapper;
using EventAPI.UI.Mappers;

namespace EventAPI.UI.DI
{
    public static class MapperDI
    {
        public static void AddMapperDI(this IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new UIModelsMapper());
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
