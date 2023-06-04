

using tk_web.DAL.Interfaces;
using tk_web.Domain.Enum;
using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Service.Interfaces;
using tk_web.Domain.ViewModels.Booking;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace tk_web.Service.Implementations
{
    public class BookingService : IBookingService 
    {
        private readonly IBaseRepository<Booking> _bookingRepository;
        private readonly ReportService _report;

        public BookingService(IBaseRepository<Booking> bookingRepository, ReportService report)
        {
            _bookingRepository = bookingRepository;
            _report = report;
        }

        public IBaseResponse<List<Booking>> GetBookings()
        {        
            try
            {
                var bookings =  _bookingRepository.GetAll().ToList();
                if (!bookings.Any())
                {
                    return new BaseResponse<List<Booking>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Booking>>()
                {
                    Data = bookings,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Booking>>()
                {
                    Description = $"[GetBookings] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<BookingViewModel>> GetBooking(int id)
        {
            try
            {
                var booking = await _bookingRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (booking == null)
                {
                    return new BaseResponse<BookingViewModel>()
                    {
                        Description = "Бронирование не найдено",
                        StatusCode = StatusCode.BookingNotFound
                    };
                }

                var participant = new Dictionary<int, string>
                {
                    { booking.Id, booking.Participant.FullName }
                };
                var event_ = new Dictionary<int, string>
                {
                    { booking.EventId, booking.Event.Name }
                };
                var equipment = new Dictionary<int, string>
                {
                    { booking.EquipmentId, booking.Equipment.Name }
                };


                var data = new BookingViewModel()
                {
                    ParticipantId = booking.ParticipantId,
                    Participant = booking.Participant.FullName,
                    //Participant = { booking.Id, booking.Participant.FullName },

                    //Participant = booking.Participant.FullName,

                    EventId = booking.EventId,
                    Event_ = booking.Event.Name,
                    //Event_ = { { booking.EventId, booking.Event.Name } },

                    //Event_ = booking.Event.Name,

                    EquipmentId = booking.EquipmentId,
                    Equipment = booking.Equipment.Name,
                    //Equipment = { { booking.EquipmentId, booking.Equipment.Name } },

                    //Equipment = booking.Equipment.Name,

                    IsuueDate = booking.IsuueDate,
                    HandoverDate = booking.HandoverDate

                };

                _report.AddActionToExcel("Получение бронирования:", $"{data.Participant} - {data.Equipment}", DateTime.Now);

                return new BaseResponse<BookingViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<BookingViewModel>()
                {
                    Description = $"[GetBooking] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Booking>> CreateBooking(BookingViewModel bookingViewModel)
        {
            try
            {
                var booking = new Booking()
                {
                    ParticipantId = bookingViewModel.ParticipantId/*.Keys.ToList()[0]*/,
                    EventId = bookingViewModel.EventId/*_.Keys.ToList()[0]*/,
                    EquipmentId = bookingViewModel.EquipmentId/*.Keys.ToList()[0]*/,
                    IsuueDate = bookingViewModel.IsuueDate,
                    HandoverDate = bookingViewModel.HandoverDate
                };

                //_report.AddActionToExcel("Создание бронирования:", $"{booking.Participant.FullName} - {booking.Equipment.Name}", DateTime.Now);
                await _bookingRepository.Create(booking);

                return new BaseResponse<Booking>()
                {
                    Data = booking,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Booking>()
                {
                    Description = $"[CreateBooking] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteBooking(int id)
        {
            try
            {
                var booking = await _bookingRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (booking == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Бронирование не найдено",
                        StatusCode = StatusCode.BookingNotFound,
                        Data = false
                    };
                }

                _report.AddActionToExcel("Удаление бронирования:", $"{booking.Participant.FullName} - {booking.Equipment.Name}", DateTime.Now);
                await _bookingRepository.Delete(booking);
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
                    Description = $"[DeleteBooking] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        

        public async Task<IBaseResponse<Booking>> EditBooking(BookingViewModel model)
        {
            try
            {
                var booking = await _bookingRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (booking == null)
                {
                    return new BaseResponse<Booking>()
                    {
                        Description = "Бронирование не найдено",
                        StatusCode = StatusCode.BookingNotFound
                    };
                }

                string old = $"{booking.Participant.FullName} - {booking.Equipment.Name}";

                booking.ParticipantId = model.ParticipantId/*.Keys.ToList()[0]*/;
                booking.EventId = model.EventId/*_.Keys.ToList()[0]*/;
                booking.EquipmentId = model.EquipmentId/*.Keys.ToList()[0]*/;
                booking.IsuueDate = model.IsuueDate;
                booking.HandoverDate = model.HandoverDate;

                _report.AddActionToExcel("Редактирование бронирования:", $"{booking.Participant.FullName} - {booking.Equipment.Name}", DateTime.Now, old);
                await _bookingRepository.Update(booking);

                return new BaseResponse<Booking>()
                {
                    Data = booking,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Booking>()
                {
                    Description = $"[EditBooking] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }            
        }
    }
}

