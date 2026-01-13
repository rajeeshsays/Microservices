using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace TransportService.Model
{
    [Table("Driver")]
    public class Driver 
    {
        [Key]
        public short ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public string AdharNo { get; set; } = string.Empty;

        [StringLength(50)]
        public string Mobile1 { get; set; } = string.Empty;
        [StringLength(50)]
        public string Mobile2 { get; set; } = string.Empty;     
        [StringLength(256)]
        public string? AddressLine1 { get; set; }

        [StringLength(256)]
        public string? AddressLine2 { get; set; }

        public int AccountId { get; set; }
    }
}
