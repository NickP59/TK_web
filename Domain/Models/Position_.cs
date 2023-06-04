using System;
using System.Collections.Generic;

namespace tk_web.Domain.Models;

public partial class Position_
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
