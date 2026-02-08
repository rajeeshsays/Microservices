public class LocationDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string DistrictId { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }   