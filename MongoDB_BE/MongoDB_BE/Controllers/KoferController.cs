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
    [ApiController]
    [Route("[controller]")]
    public class KoferController : ControllerBase
    {
       /* [HttpPost]
        [Route("KreirajKolekcijuKofera")]
       public ActionResult KreirajKolekcijuKofera()
        {
            try
            {
                DataProvider.KreirajKolekcijuKofera();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }*/

        [HttpGet]
        [Route("VratiSveKofere")]
        public ActionResult VratiKofere()
        {
            try
            {
                return Ok(DataProvider.VratiSveKofere());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("KreirajKofer")]
        public ActionResult KreirajKofer([FromBody] Kofer kofer)
        {
            try
            {
                DataProvider.KreirajKofer(kofer);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiKofer/{koferId}")]
        public ActionResult ObrisiKofer([FromRoute(Name = "koferId")] string koferId)
        {
            try
            {
                DataProvider.ObrisiKofer(new ObjectId(koferId));
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajTipKofera/{idKofera}/{newTip}")]
        public ActionResult AzurirajTipKofera([FromRoute] string idKofera,
                                                            [FromRoute(Name = "newTip")] string newTip)
        {
            try
            {
                DataProvider.AzurirajTipKofera(new ObjectId(idKofera), newTip);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}
