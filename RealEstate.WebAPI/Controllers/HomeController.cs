namespace RealEstate.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        [HttpGet("/", Name = "Home")]
        public IActionResult Home()
        {
            return View();
        }
    }
}