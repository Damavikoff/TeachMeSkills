using AutoMapper;
using WebDiary.Models;
using WebDiary.BLL.Models;

namespace WebDiary.Services
{
    public class EventViewModelMappingProfile : Profile
    {
        public EventViewModelMappingProfile() 
        {

            CreateMap<GroupViewModel, GroupDTO>();
            CreateMap<GroupDTO, GroupViewModel>();

            CreateMap<EventViewModel, EventDTO>();
            CreateMap<EventDTO, EventViewModel>();

            CreateMap<CommentViewModel, CommentDTO>();
            CreateMap<CommentDTO, CommentViewModel>();
        }
    }
}