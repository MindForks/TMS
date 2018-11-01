using AutoMapper;

namespace TMS.Bootstrap.Automapper
{
    public class TMSAutoMapper : TMS.Interfaces.IMapper
    {
        private readonly IMapper mapper;

        public TMSAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<AutomapperProfile>()
            );
            mapper = config.CreateMapper();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }
    }
}
