using AutoMapper;
using WebDiary.Models;
using WebDiary.BLL.Models;

namespace WebDiary.Services
{
    public class EventViewModelMappingProfile : Profile
    {
        public EventViewModelMappingProfile() 
        {
            CreateMap<EventViewModel, EventDTO>();
            CreateMap<EventDTO, EventViewModel>();
        }
    }
}