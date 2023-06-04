﻿using System;
using System.Collections.Generic;

namespace tk_web.Domain.Models;

public partial class EquipmentPlace
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
