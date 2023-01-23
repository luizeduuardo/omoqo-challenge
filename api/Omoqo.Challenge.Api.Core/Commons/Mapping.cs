namespace Omoqo.Challenge.Api.Core.Commons;

public static class Mapping
{
    private static readonly Lazy<IMapper> Lazy = new(() =>
    {
        MapperConfiguration config = new(cfg => { cfg.AddProfile<MappingProfile>(); });
        IMapper mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper => Lazy.Value;
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ShipAddModel, Ship>();
    }
}