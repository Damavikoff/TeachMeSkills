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

            CreateMap<CommentDTO, Comment>();
            CreateMap<CommentDTO, Comment>().ReverseMap();

            CreateMap<UserDTO, User>();
            CreateMap<UserDTO, User>().ReverseMap();

            CreateMap<FriendsDTO, Friends>();
            CreateMap<FriendsDTO, Friends>().ReverseMap();
        }
    }
}