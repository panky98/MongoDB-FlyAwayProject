using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_BE.Controllers
{
    public class KomentarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
