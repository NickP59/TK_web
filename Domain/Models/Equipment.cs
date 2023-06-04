using System;
using System.Collections.Generic;

namespace tk_web.Domain.Models;

public partial class Equipment
{
    public int Id { get; set; }

    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public int PlaceId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual EquipmentPlace Place { get; set; } = null!;

    public virtual EquipmentType Type { get; set; } = null!;
}
