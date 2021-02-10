using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProizvodController : ControllerBase
    {
        [HttpPost]
        [Route("KreirajKolekcijuProizvoda")]
        public ActionResult KreirajKolekcijuProizvoda()
        {
            try
            {
                DataProvider.KreirajKolekcijuProizvoda();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("KreirajProizvod")]
        public ActionResult KreirajProizvod([FromBody] ProizvodDTO proizvod)
        {
            try
            {
                Proizvod p = new Proizvod
                {
                    Id = proizvod.Id,
                    cena= proizvod.cena,
                    SlikaBytes = Convert.FromBase64String(proizvod.SlikaBytesBase64),
                    tip = proizvod.tip,
                    kolicina = proizvod.kolicina
                };

                DataProvider.KreirajProizvod(p);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiProizvode")]
        public ActionResult VratiProizvode()
        {
            try
            {
                return Ok(DataProvider.VratiProizvode());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajKolicinuProizvoda/{idProizvoda}/{newKolicina}")]
        public ActionResult AzurirajStatusRezervaciji([FromRoute(Name = "idProizvoda")] ObjectId idProizvoda,
                                                    [FromRoute(Name = "newKolicina")] int newKolicina)
        {
            try
            {
                DataProvider.AzurirajKolicinuProizvoda(idProizvoda, newKolicina);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiProizvod/{proizvodId}")]
        public ActionResult ObrisiProizvod([FromRoute(Name = "proizvodId")] ObjectId proizvodId)
        {
            try
            {
                DataProvider.ObrisiProizvod(proizvodId);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }



    }
}
