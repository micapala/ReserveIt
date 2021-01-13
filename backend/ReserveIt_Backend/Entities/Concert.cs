using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Models
{
    public class Concert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required()]
        public String Name { get; set; }

        [Required()]
        [ForeignKey("BandId")]
        public Band Band { get; set; }

        [Required()]
        public DateTime Date { get; set; }

        [Required()]
        public float TicketPrice { get; set; }
    }
}
