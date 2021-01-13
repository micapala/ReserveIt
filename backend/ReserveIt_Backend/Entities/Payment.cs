using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ReserveIt_Backend.Models;

namespace ReserveIt_Backend.Entities
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required()]
        public string ControlString { get; set; }

        [Required()]
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required()]
        [ForeignKey("ConcertId")]
        public Concert Concert { get; set; }

        [Required()]
        public float TotalAmount { get; set; }

        [Required()]
        public float TotalDue { get; set; }
    }
}
