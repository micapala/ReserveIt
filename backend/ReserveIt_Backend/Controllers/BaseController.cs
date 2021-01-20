using Microsoft.AspNetCore.Mvc;
using ReserveIt_Backend.Models;

namespace ReserveIt_Backend.Controllers
{  
    [Controller]
    public class BaseController : ControllerBase
    {
        public User User => (User)HttpContext.Items["User"];
    }
}
