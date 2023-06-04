using System;
using System.Collections.Generic;

namespace tk_web.Domain.Models;

public partial class Participant
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int GroupId { get; set; }

    public int? PositionId { get; set; }

    public string? SocialNetworkLink { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Group Group { get; set; } = null!;

    public virtual Position_? Position { get; set; }
}
