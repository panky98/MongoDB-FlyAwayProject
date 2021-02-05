using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PutnikController : Controller
    {
        [HttpGet]
        [Route("VratiSvePutnike")]
        public ActionResult VratiSvePutnike()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSvePutnike());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiPutnikeRodjenePosle/{godina}")]
        public ActionResult VratiPutnikeRodjenePosle([FromRoute] int godina)
        {
            try
            {
                return new JsonResult(DataProvider.VratiPutnikeRodjenePosle(godina));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }

        }

        [HttpPost]
        [Route("KreirajKolekcijuPutnika")]
        public ActionResult KreirajKolekcijuPutnika()
        {
            try
            {
                DataProvider.KreirajKolekcijuPutnika();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("KreirajPutnika")]
        public ActionResult KreirajPutnika([FromBody] Putnik putnik)
        {
            try
            {

                Putnik putnik1 = new Putnik
                {
                    Id = putnik.Id,
                    ime = putnik.ime,
                    prezime = putnik.prezime,
                    godinaRodjenja = putnik.godinaRodjenja, 
                    pol = putnik.pol
                };

                DataProvider.KreirajPutnika(putnik1);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}
