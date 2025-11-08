using Mapster;
using Portal.Api.DTOs;
using Portal.Api.Models;

namespace Portal.Api.Mappings
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Entity -> DTO
            config.NewConfig<UcHarfKelime, UcHarfKelimeDto>()
                .Map(dest => dest.Id, src => src.UcHarfKelimeId);

            // DTO -> Entity
            config.NewConfig<UcHarfKelimeDto, UcHarfKelime>()
                .Map(dest => dest.UcHarfKelimeId, src => src.Id);
        }
    }
}
