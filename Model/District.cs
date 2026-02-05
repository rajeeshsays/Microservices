
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class District 
    {
        [Key]
        public string ID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;


        [ForeignKey("State")]
        public string StateId { get; set; } = string.Empty;

        public virtual State? State { get; set; }
    }