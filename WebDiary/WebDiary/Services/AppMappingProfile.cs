using AutoMapper;
using WebDiary.Models;
using Data.Models;

namespace WebDiary.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() 
        {
            CreateMap<Event, EventDTO>();
            CreateMap<Event, EventDTO>().ReverseMap();
        }
    }
}
