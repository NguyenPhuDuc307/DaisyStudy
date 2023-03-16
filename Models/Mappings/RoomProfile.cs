using AutoMapper;
using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.Rooms;

namespace DaisyStudy.Models.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomViewModel>();
        CreateMap<RoomViewModel, Room>();
    }
}
