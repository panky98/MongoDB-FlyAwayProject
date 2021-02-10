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
    public class LetController : ControllerBase
    {

        [HttpPost]
        [Route("KreirajLet")]
        public ActionResult KreirajLet([FromBody]Let let)
        {
            try
            {
                DataProvider.KreirajLet(let);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }

        [HttpGet]
        [Route("VratiSveLetove")]
        public ActionResult VratiSveLetove()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveLetove());
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiSveLetoveSaObjectId")]
        public ActionResult VratiSveLetoveSaObjectId()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveLetoveSaObjectId());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiGotoveLetove")]
        public ActionResult VratiGotoveLetove()
        {
            try
            {
                return new JsonResult(DataProvider.VratiGotoveLetove());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiTrenutneLetove")]
        public ActionResult VratiTrenutneLetove()
        {
            try
            {
                return new JsonResult(DataProvider.VratiTrenutneLetove());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiLet/{id}")]
        public ActionResult VratiLet([FromRoute]string id)
        {
            try
            {
                return new JsonResult(DataProvider.VratiLet(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajLet/{id}")]
        public ActionResult AzurirajLet([FromRoute]string id, [FromBody]LetDTO let)
        {
            try
            {
                DataProvider.AzurirajLet(id, let);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiLet/{id}")]
        public ActionResult ObrisiLet([FromRoute]string id)
        {
            try
            {
                DataProvider.ObrisiLet(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
