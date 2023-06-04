using System;
using System.Collections.Generic;

namespace tk_web.Domain.Models;

public partial class Organizer
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int PositionId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string SocialNetworkLink { get; set; } = null!;
}
