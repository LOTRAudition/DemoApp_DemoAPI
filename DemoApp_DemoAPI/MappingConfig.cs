using AutoMapper;
using DemoApp_DemoAPI.Models;
using DemoApp_DemoAPI.Models.Dto;

namespace DemoApp_DemoAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Demo, DemoDTO>();
            CreateMap<DemoDTO,Demo>();

            CreateMap<Demo, DemoCreateDTO>().ReverseMap();
            CreateMap<Demo, DemoUpdateDTO>().ReverseMap();


            CreateMap<DemoNumber, DemoNumberDTO>().ReverseMap();
            CreateMap<DemoNumber, DemoNumberCreateDTO>().ReverseMap();
            CreateMap<DemoNumber, DemoNumberUpdateDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
