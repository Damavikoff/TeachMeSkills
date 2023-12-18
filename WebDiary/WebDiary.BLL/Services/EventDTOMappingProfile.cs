using AutoMapper;
using WebDiary.BLL.Models;
using WebDiary.DAL.Models;

namespace WebDiary.BLL.Services
{
    public class EventDTOMappingProfile : Profile
    {
        public EventDTOMappingProfile() 
        {
            CreateMap<EventDTO, Event>();
            CreateMap<EventDTO, Event>().ReverseMap();

            CreateMap<GroupDTO, Group>();
            CreateMap<GroupDTO, Group>().ReverseMap();
        }
    }
}