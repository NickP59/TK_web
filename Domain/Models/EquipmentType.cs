using System;
using System.Collections.Generic;

namespace tk_web.Domain.Models;

public partial class EquipmentType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
