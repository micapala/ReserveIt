using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.dtos
{
    public class CreateUserRequest
    {
        [Required]
        public String username { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        [EmailAddress]
        public String email { get; set; }

        [Required]
        public String name { get; set; }

        [Required]
        public String surname { get; set; }
    }
}
