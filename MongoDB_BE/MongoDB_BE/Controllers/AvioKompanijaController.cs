using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AvioKompanijaController : ControllerBase
    {
        [HttpPost]
        [Route("KreirajAvioKompaniju")]
        public ActionResult KreirajAvioKompaniju([FromBody] AvioKompanija avioKompanija)
        {
            try
            {
                DataProvider.KreirajAvioKompaniju(avioKompanija);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }

        [HttpGet]
        [Route("VratiAvioKompanije")]
        public ActionResult VratiAvioKompanije()
        {
            try
            {
                return new JsonResult(DataProvider.VratiAvioKompanije());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiAvioKompaniju/{id}")]
        public ActionResult VratiAvioKompaniju([FromRoute]string id)
        {
            try
            {
                return new JsonResult(DataProvider.VratiAvioKompaniju(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
        [HttpPut]
        [Route("AzurirajAvioKompaniju/{id}")]
        public ActionResult AzurirajAvioKompaniju([FromRoute] string id, [FromBody] AvioKompanijaDTOUpdate avioKompanija)
        {
            try
            {
                DataProvider.AzurirajAvioKompaniju(id, avioKompanija);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiAvioKompanijuId/{id}")]
        public IActionResult ObrisiAvioKompanijuId([FromRoute]string id)
        {
            try
            {
                DataProvider.ObrisiAvioKompanijuId(id);
                return Ok();
            }
             catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
