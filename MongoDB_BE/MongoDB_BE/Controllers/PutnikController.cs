using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        [HttpGet]
        [Route("VratiPutnikeSaLetom/{kod}")]
        public ActionResult VratiPutnikeSaLetom([FromRoute(Name ="kod")] String kod)
        {
            try
            {
                return new JsonResult(DataProvider.VratiPutnikeSaLetom(kod));
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
                ObjectId retVal=DataProvider.KreirajPutnika(putnik);
                return new JsonResult(retVal.ToString());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiPutnika/{jmbg}")]
        public ActionResult ObrisiPutnika([FromRoute(Name = "jmbg")] String jmbg)
        {
            try
            {
                DataProvider.ObrisiPutnika(jmbg);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("DodajRezervacijuPutniku/{kod}/{jmbg}")]
        public ActionResult DodajRezervacijuPutniku([FromRoute(Name = "kod")] String kod, 
                                                          [FromRoute(Name = "jmbg")] String jmbg)
        {
            try
            {
                DataProvider.DodajRezervacijuPutniku(kod, jmbg);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajPutnika/{jmbg}/{prezime}")]
        public ActionResult AzurirajPutnika([FromRoute(Name = "jmbg")] String jmbg,
                                                    [FromRoute(Name = "prezime")] string prezime)
        {
            try
            {
                DataProvider.AzurirajPutnika(jmbg, prezime);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}
