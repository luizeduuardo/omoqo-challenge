namespace Omoqo.Challenge.Api.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserAuthenticateRequest, UserAuthenticateModel>();

        CreateMap<Ship, ShipGetResponse>();
        CreateMap<Ship, ShipListItemResponse>();
        CreateMap<ShipAddRequest, ShipAddModel>();
        CreateMap<ShipUpdateRequest, ShipUpdateModel>();
    }
}