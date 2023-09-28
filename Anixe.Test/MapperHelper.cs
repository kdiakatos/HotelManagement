using Anixe.Business;
using AutoMapper;

namespace Anixe.Test
{
    public static class MapperHelper
    {
        public static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping());
            });

            return mapperConfig.CreateMapper();
        }
    }
}
