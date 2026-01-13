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

    }


}
