using Mapster;
using Portal.Api.Models;
using Portal.Api.Models.Responses;

namespace Portal.Api.Mappings
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Kelime, KelimeResponse>();
            config.NewConfig<Kategori, KategoriResponse>();
        }
    }
}
