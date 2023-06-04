namespace tk_web.Domain.ViewModels.Participant
{
    public class ParticipantViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public int GroupId { get; set; }

        public int? PositionId { get; set; }

        public string? SocialNetworkLink { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
