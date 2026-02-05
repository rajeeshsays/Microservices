using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportService.Model
{
    [Table("Location")]
    public class Location 
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

         [ForeignKey("District")]
        public string DistrictId { get; set; } = string.Empty;

        
        public virtual District? District { get; set; }

        public bool IsActive { get; set; } = true;


    }


}
