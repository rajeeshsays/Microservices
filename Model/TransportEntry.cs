using global::TransportService.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TransportService.Model
{
        [Table("TransportEntry")]
        public class TransportEntry 
        {
            [Key]
            public long ID { get; set; }

            public DateTime Date { get; set; }

            // Foreign Key
            public short VehicleId { get; set; }

            // Navigation property
            [ForeignKey("VehicleId")]
            public Vehicle? Vehicle { get; set; }
            public short VehicleTypeId { get; set; }

            [ForeignKey("VehicleTypeId")]
            public VehicleType? VehicleType { get; set; }

            public short DriverId { get; set; }

            [ForeignKey("DriverId")]
            public Driver? Driver { get; set; }

            public short Party1 { get; set; }

            
            public long DestinationGroupId { get; set; }

            [ForeignKey("Party1")]
            public Party? Party_Party1 { get; set; }

            [ForeignKey("DestinationGroupId")]
            public ICollection<DestinationGroup> DestinationGroups { get; set; }
            = new List<DestinationGroup>();

            public int From { get; set; }

            public int To { get; set; }

            [ForeignKey("From")]
            public Location? LocationFrom { get; set; }

            [ForeignKey("To")]
            public Location? LocationTo { get; set; }

            public decimal StartKM { get; set; }

            public decimal CloseKM { get; set; }

            public decimal Total { get; set; }

            public decimal Loading { get; set; }

            public decimal Unloading { get; set; }

            public decimal LoadingCommision { get; set; }

            public decimal UnloadingCommision { get; set; }


            //public string ReturnDestination { get; set; }
            public int ReturnDestinationId { get; set; }

            [ForeignKey("ReturnDestinationId")]
            public Location? ReturnDestination { get; set; }
            public short HaltDays { get; set; }

            public decimal Rent { get; set; }

            public string? Narration { get; set; }
            

            public string? Other { get; set; }

            public int AccountId { get; set; }

    

        }
    }


