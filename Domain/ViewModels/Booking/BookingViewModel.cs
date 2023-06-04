using System.ComponentModel.DataAnnotations;
using tk_web.Domain.Models;

namespace tk_web.Domain.ViewModels.Booking
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Выберите участника!")]
        public int ParticipantId { get; set; }
        public string? Participant { get; set; }
        //public Dictionary<int, string> Participant { get; set; }

        //public string Participant { get; set; }

        [Required(ErrorMessage = "Выберите мероприятие!")]
        public int EventId { get; set; }
        public string? Event_ { get; set; }
        //public Dictionary<int, string> Event_ { get; set; }

        //public string Event_ { get; set; }

        [Required(ErrorMessage = "Выберите снаряжение!")]
        public int EquipmentId { get; set; }
        public string? Equipment { get; set; }
        //public Dictionary<int, string> Equipment { get; set; }

        //public string Equipment { get; set; }


        //[Required(ErrorMessage = "Введите дату выдачи!")]
        public DateTime? IsuueDate { get; set; }

        //[Required(ErrorMessage = "Введите дату сдачи!")]
        public DateTime? HandoverDate { get; set; }

    }
}
