public class VehicleDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }