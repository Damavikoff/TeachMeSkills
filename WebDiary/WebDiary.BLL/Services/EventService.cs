using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDiary.BLL.Models;
using WebDiary.BLL.Services.Interfaces;
using WebDiary.DAL.Models;

namespace WebDiary.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly WebDiaryContext _webDiaryContext;
        public EventService(WebDiaryContext webDiaryContext, IMapper mapper)
        {
            _webDiaryContext = webDiaryContext;
            _mapper = mapper;
        }

        public async Task<ServiceDataResponse<List<EventDTO>>> LoadEventsAsync()
        {
            var objs = await _webDiaryContext.Events.ToListAsync();

            if (objs == null)
            {
                return new ServiceDataResponse<List<EventDTO>>
                {
                    Message = "There are no events!",
                    Succeeded = false
                };
            }

            var objsDTO = _mapper.Map<List<EventDTO>>(objs);

            return new ServiceDataResponse<List<EventDTO>>
            {
                Succeeded = true,
                Data = objsDTO
            };
        }

        public async Task<ServiceDataResponse<EventDTO>> GetEventAsync(Guid eventId)
        {
            var obj = await _webDiaryContext.Events.FirstOrDefaultAsync(p => p.Id == eventId);

            if (obj == null)
            {
                return new ServiceDataResponse<EventDTO>
                {
                    Message = "Event is not founded!",
                    Succeeded = false
                };
            }

            var objDTO = _mapper.Map<EventDTO>(obj);
            return new ServiceDataResponse<EventDTO>
            {
                Succeeded = true,
                Data = objDTO
            };
        }

        public async Task<ServiceResponse> CreateEventAsync(EventDTO eventModel)
        {
            try
            {
                eventModel.Id = Guid.NewGuid();
                var obj = _mapper.Map<Event>(eventModel);
                _webDiaryContext.Events.Add(obj);
                await _webDiaryContext.SaveChangesAsync(); //request to DB is here => async

                return new ServiceResponse
                {
                    Succeeded = true,
                    Message = "Event successfully created!"
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse
                {
                    Succeeded = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse> UpdateEventAsync(EventDTO eventModel)
        {
            try
            {
                var obj = _mapper.Map<Event>(eventModel);
                _webDiaryContext.Events.Update(obj);
                await _webDiaryContext.SaveChangesAsync();

                return new ServiceResponse
                {
                    Succeeded = true,
                    Message = "Event successfully updated!"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    Succeeded = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse> DeleteEventAsync(Guid eventId)
        {
            //can send null guid (delete when form is already open)
            var obj = await _webDiaryContext.Events.FirstOrDefaultAsync(p => p.Id == eventId);

            if (obj == null)
            {
                return new ServiceResponse
                {
                    Succeeded = false,
                    Message = "Event is not founded!"
                };
            }

            _webDiaryContext.Events.Remove(obj);
            await _webDiaryContext.SaveChangesAsync();

            return new ServiceResponse
            {
                Succeeded = true,
                Message = "Event successfully deleted!"
            };
        }
    }
}