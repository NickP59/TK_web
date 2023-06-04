using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Event;

namespace tk_web.Service.Interfaces
{
    public interface IEventService
    {
        IBaseResponse<List<Event>> GetEvents();

        Task<IBaseResponse<EventViewModel>> GetEvent(int id);

        Task<IBaseResponse<Event>> CreateEvent(EventViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteEvent(int id);

        Task<IBaseResponse<Event>> EditEvent(EventViewModel model);

        Task<BaseResponse<Dictionary<int, string>>> GetEvent(string term);
    }
}
