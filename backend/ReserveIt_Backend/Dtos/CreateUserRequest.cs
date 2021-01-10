using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.dtos
{
    public class CreateUserRequest
    {
        public String username { get; set; }

        public string password { get; set; }

        public String email { get; set; }

        public String name { get; set; }

        public String surname { get; set; }
    }
}
