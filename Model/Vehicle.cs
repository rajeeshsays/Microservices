using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Model
{
    public class Vehicle 
    {
        [Key]
        public Int16 ID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Model {get;set;} = string.Empty;

        [Column(TypeName = "nvarchar(50)")]
        public string Registration { get; set; } = string.Empty;

        public Int16 TypeId { get; set; }

        [ForeignKey("TypeId")]
        public  VehicleType? VehicleType { get; set; }
      
    }

    public class VehicleType 
    {
        [Key]
        public Int16 ID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Desc { get; set; } = string.Empty;

        public int AccountId { get; set; }

    }



}
