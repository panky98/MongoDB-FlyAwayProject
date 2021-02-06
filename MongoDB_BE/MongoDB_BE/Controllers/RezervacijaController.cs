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
    public class RezervacijaController : ControllerBase
    {
        [HttpPost]
        [Route("KreirajKolekcijuRezervacija")]
        public ActionResult KreirajKolekcijuRezervacija()
        {
            try
            {
                DataProvider.KreirajKolekcijuRezervacija();
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("KreirajRezervaciju")]
        public ActionResult KreirajRezervaciju([FromBody] RezervacijaDTO rezervacija)
        {
            try
            {
                String[] timeNow = DateTime.Now.ToLongTimeString().Split(":");
                String[] dateNow = DateTime.Now.ToShortDateString().Split("/");
                Rezervacija r = new Rezervacija
                {
                    Id = rezervacija.Id,
                    BrojSedista = rezervacija.BrojSedista,
                    PasosBytes = Convert.FromBase64String(rezervacija.PasosBytesBase64),
                    CovidTestBytes = Convert.FromBase64String(rezervacija.CovidTestBytesBase64),
                    Status = rezervacija.Status,
                    KodRezervacije = "RE"+dateNow[0]+dateNow[1]+dateNow[2]+timeNow[0]+timeNow[1]+timeNow[2].ElementAt(0)+timeNow[2].ElementAt(1),
                    ListaProizvoda = rezervacija.ListaProizvoda,
                    ListaKofera = rezervacija.ListaKofera,
                    Putnik = rezervacija.Putnik,
                    Let = rezervacija.Let
                };

                DataProvider.KreirajRezervaciju(r);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
        [HttpGet]
        [Route("VratiRezervacijuPrekoKoda/{kodRezervacije}")]
        public ActionResult VratiRezervacijuPrekoId([FromRoute(Name = "kodRezervacije")] string kodRezervacije)
        {
            try
            {
                 return Ok(DataProvider.VratiRezervacijuPrekoKoda(kodRezervacije));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiRezervacije")]
        public ActionResult VratiRezervacije()
        {
            try
            {
                return Ok(DataProvider.VratiRezervacije());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObirisRezervaciju/{kodRezervacije}")]
        public ActionResult ObrisiRezervaciju([FromRoute(Name ="kodRezervacije")]string kodRezervacije)
        {
            try
            {
                DataProvider.ObrisiRezervaciju(kodRezervacije);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajStatusRezervaciji/{kodRezervacije}/{newStatus}")]
        public ActionResult AzurirajStatusRezervaciji([FromRoute(Name = "kodRezervacije")] string kodRezervacije,
                                                    [FromRoute(Name = "newStatus")] string newStatus)
        {
            try
            {
                DataProvider.AzurirajRezervaciju(kodRezervacije,newStatus);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}
