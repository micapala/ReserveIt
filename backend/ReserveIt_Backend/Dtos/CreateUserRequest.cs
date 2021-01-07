using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.dtos
{
    public class CreateUserRequest
    {
        public int Id { get; set; }

        public String login { get; set; }

        public String e_mail { get; set; }

        public String password { get; set; }

        public String name { get; set; }

        public String surname { get; set; }
    }
}
