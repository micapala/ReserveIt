﻿using ReserveIt_Backend.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required()]
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required()]
        [ForeignKey("ConcertId")]
        public Concert Concert { get; set; }

        [Required()]
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }

        [Required()]
        public DateTime ReservationDate { get; set; }
    }
}
