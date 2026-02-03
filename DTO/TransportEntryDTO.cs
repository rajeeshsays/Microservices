using global::TransportService.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TransportService.Model
{
        public class TransportEntryDto
        {
            
            public long ID { get; set; } = 0;

            public DateTime Date { get; set; } = DateTime.Now;

            // Foreign Key
            public short VehicleId { get; set; }

            // Navigation property
            public short VehicleTypeId { get; set; }

            

            public short DriverId { get; set; }

            public short Party1 { get; set; }

            
            public long DestinationGroupId { get; set; }

            public short[] DestinationGroups { get; set; }
            public int From { get; set; }

            public int To { get; set; }

            public decimal StartKM { get; set; }

            public decimal CloseKM { get; set; }

            public decimal Total { get; set; }

            public decimal Loading { get; set; }

            public decimal Unloading { get; set; }

            public decimal LoadingCommision { get; set; }

            public decimal UnloadingCommision { get; set; }


            //public string ReturnDestination { get; set; }
            public int ReturnDestinationId { get; set; }

            public short HaltDays { get; set; }

            public decimal Rent { get; set; }

            public string? Narration { get; set; }
            

            public string? Other { get; set; }

            public int AccountId { get; set; }
   

        }
    }


