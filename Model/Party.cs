using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Model
{
    public class Party 
    {
        [Key]
        public short ID { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        [Required]
        public string Code {get;set;} = string.Empty;


        [StringLength(250)]
        [Required]
        public string AddressLine1 { get; set; } = string.Empty;

        [StringLength(250)]
        public string? AddressLine2 { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; } = string.Empty;

        [StringLength(50)]
        [Required]
        public string Pincode { get; set; } = string.Empty;

        public int AccountId { get; set; }
  
        [StringLength(100)]
        public string? GstNo {get;set;}

        [StringLength(80)]
        [Required]
        public string? OfficePhone {get;set;}

        [StringLength(50)]
        public string? ContactPerson {get;set;}

        public bool IsActive  {get;set;} = true;
    }
}
