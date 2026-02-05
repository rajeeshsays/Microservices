public class DriverDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }