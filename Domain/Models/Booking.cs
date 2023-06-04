using System;
using System.Collections.Generic;

namespace tk_web.Domain.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int ParticipantId { get; set; }

    public int EventId { get; set; }

    public int EquipmentId { get; set; }

    public DateTime? IsuueDate { get; set; }

    public DateTime? HandoverDate { get; set; }

    public virtual Equipment Equipment { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Participant Participant { get; set; } = null!;
}
