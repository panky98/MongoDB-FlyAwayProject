using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

                Rezervacija r = new Rezervacija
                {
                    Id = rezervacija.Id,
                    BrojSedista = rezervacija.BrojSedista,
                    PasosBytes = Convert.FromBase64String(rezervacija.PasosBytesBase64),
                    CovidTestBytes = Convert.FromBase64String(rezervacija.CovidTestBytesBase64),
                    Status = rezervacija.Status,
                    KodRezervacije = rezervacija.KodRezervacije,
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
    }
}
