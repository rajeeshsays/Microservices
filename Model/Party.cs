using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Model
{
    public class Party 
    {
        [Key]
        public short ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? AddressLine1 { get; set; }

        [StringLength(250)]
        public string? AddressLine2 { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; } = string.Empty;

        [StringLength(50)]
        public string Pincode { get; set; } = string.Empty;

        public int AccountId { get; set; }
    }
}
