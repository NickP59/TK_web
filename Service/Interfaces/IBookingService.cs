
using tk_web.DAL.Interfaces;
using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Booking;

namespace tk_web.Service.Interfaces
{
    public interface IBookingService
    {
        IBaseResponse<List<Booking>> GetBookings();

        Task<IBaseResponse<BookingViewModel>> GetBooking(int id);

        Task<IBaseResponse<Booking>> CreateBooking(BookingViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteBooking(int id);

        Task<IBaseResponse<Booking>> EditBooking(BookingViewModel model);
    }
}
