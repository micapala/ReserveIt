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

        public String Name { get; set; }

        [NotNull]
        [ForeignKey("BandId")]
        public Band Band { get; set; }

        public DateTime Date { get; set; }

        public float TicketPrice { get; set; }
    }
}
