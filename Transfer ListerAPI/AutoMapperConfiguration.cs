using AutoMapper;
using Transfer_ListerAPI.Models;
using Transfer_ListerAPI.Models.DTOs;

namespace Transfer_ListerAPI
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Player, PlayerDTO>().ReverseMap();
            CreateMap<CreatePlayerDTO, Player>();
            CreateMap<UpdatePlayerDTO, Player>();
        }
    }
}
