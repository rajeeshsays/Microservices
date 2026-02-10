public class DriverDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AdhaarNo {get;set;} = string.Empty;

        public string Mobile1 {get;set;} = string.Empty;
        public string? Mobile2 {get;set;} = string.Empty;
        public string AddressLine1 {get;set;} = string.Empty;

        public string? AddressLine2 {get;set;} = string.Empty;

        public string LicenceNo { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }