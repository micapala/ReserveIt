using Microsoft.AspNetCore.Mvc;
using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Controllers
{  
    [Controller]
    public class BaseController : ControllerBase
    {
        public User User => (User)HttpContext.Items["User"];
    }
}
