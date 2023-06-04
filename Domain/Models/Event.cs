using System;
using System.Collections.Generic;

namespace tk_web.Domain.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? CountOfPeople { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
