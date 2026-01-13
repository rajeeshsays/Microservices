using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TransportService.Model
{
    public class DestinationGroup
    {
        [Key]
        public long ID { get; set; }

        public long TransportId {get;set;}
        public short DestinationId { get; set; }

        [ForeignKey("DestinationId")]
        public Party? Party { get; set; }
        
    }


}
