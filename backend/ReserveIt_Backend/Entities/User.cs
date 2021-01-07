using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ReserveIt_Backend.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public String Email { get; set; }

        public String Name { get; set; }

        public String Surname { get; set; }
    }
}
