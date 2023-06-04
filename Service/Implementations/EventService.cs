using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.DAL.Repositories;
using tk_web.Domain.Enum;
using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Event;
using tk_web.Domain.ViewModels.Participant;
using tk_web.Service.Interfaces;

namespace tk_web.Service.Implementations
{
    public class EventService : IEventService
    {
        private readonly IBaseRepository<Event> _EventRepository;

        public EventService(IBaseRepository<Event> EventRepository)
        {
            _EventRepository = EventRepository;
        }

        public IBaseResponse<List<Event>> GetEvents()
        {

            try
            {
                var events = _EventRepository.GetAll().ToList();
                if (!events.Any())
                {
                    return new BaseResponse<List<Event>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Event>>()
                {
                    Data = events,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Event>>()
                {
                    Description = $"[GetEvents] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<EventViewModel>> GetEvent(int id)
        {
            try
            {
                var Event = await _EventRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (Event == null)
                {
                    return new BaseResponse<EventViewModel>()
                    {
                        Description = "Мероприятие не найдено",
                        StatusCode = StatusCode.EventNotFound
                    };
                }

                var data = new EventViewModel()
                {
                    Name = Event.Name,
                    StartDate = Event.StartDate,
                    EndDate = Event.EndDate,
                    CountOfPeople = Event.CountOfPeople
                };

                return new BaseResponse<EventViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EventViewModel>()
                {
                    Description = $"[GetEvent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Event>> CreateEvent(EventViewModel eventViewModel)
        {
            try
            {
                var Event = new Event()
                {
                    Name = eventViewModel.Name,
                    StartDate = eventViewModel.StartDate,
                    EndDate = eventViewModel.EndDate,
                    CountOfPeople = eventViewModel.CountOfPeople
                };

                await _EventRepository.Create(Event);

                return new BaseResponse<Event>()
                {
                    Data = Event,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Event>()
                {
                    Description = $"[CreateEvent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteEvent(int id)
        {
            try
            {
                var Event = await _EventRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (Event == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Мероприятие не найдено",
                        StatusCode = StatusCode.EventNotFound,
                        Data = false
                    };
                }

                await _EventRepository.Delete(Event);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteEvent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        public async Task<IBaseResponse<Event>> EditEvent(EventViewModel model)
        {
            try
            {
                var Event = await _EventRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (Event == null)
                {
                    return new BaseResponse<Event>()
                    {
                        Description = "Мероприятие не найдено",
                        StatusCode = StatusCode.EventNotFound
                    };
                }

                Event.Name = model.Name;
                Event.StartDate = model.StartDate;
                Event.EndDate = model.EndDate;
                Event.CountOfPeople = model.CountOfPeople;

                await _EventRepository.Update(Event);

                return new BaseResponse<Event>()
                {
                    Data = Event,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Event>()
                {
                    Description = $"[EditEvent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<int, string>>> GetEvent(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var events = await _EventRepository.GetAll()
                    .Select(x => new EventViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        CountOfPeople = x.CountOfPeople
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = events;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
