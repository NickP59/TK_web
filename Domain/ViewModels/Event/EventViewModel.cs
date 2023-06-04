namespace tk_web.Domain.ViewModels.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? CountOfPeople { get; set; }
    }
}
