namespace tk_web.Domain.ViewModels.Equipment
{
    public class EquipmentViewModel
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public int PlaceId { get; set; }

    }
}
