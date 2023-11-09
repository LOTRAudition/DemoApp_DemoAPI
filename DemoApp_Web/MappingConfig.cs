using AutoMapper;
using DemoApp_Web.Models.Dto;

namespace DemoApp_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<DemoDTO,DemoCreateDTO>().ReverseMap();
            CreateMap<DemoDTO, DemoUpdateDTO>().ReverseMap();

            CreateMap<DemoNumberDTO, DemoNumberCreateDTO>().ReverseMap();
            CreateMap<DemoNumberDTO, DemoNumberUpdateDTO>().ReverseMap();
        }
    }
}
