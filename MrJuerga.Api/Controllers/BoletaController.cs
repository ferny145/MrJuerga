using MrJuerga.Entity;
using MrJuerga.Service;
using Microsoft.AspNetCore.Mvc;

namespace MrJuerga.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletaController: ControllerBase
    {
         private IBoletaService boletaService;

        public BoletaController(IBoletaService boletaService)
        {
            this.boletaService = boletaService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                boletaService.GetAll()
            );
        }

         [HttpGet("FetchTop5Customers")]
        public ActionResult FetchTop5Customers()
        {
            return Ok(
                boletaService.FetchTop5Customers()
            );
        }

        [HttpGet("FetchTop5Products")]
        public ActionResult FetchTop5Products(string inicio, string fin)
        {
            return Ok(
                boletaService.FetchTop5Products(inicio,fin)
            );
        }

        [HttpGet("FetchByStatus")]
        public ActionResult FetchByStatus(string estado)
        {
            return Ok(
                boletaService.FetchByStatus(estado)
            );
        }

        [HttpPost]
        public ActionResult Post([FromBody] Boleta boleta)
        {
            return Ok(
                boletaService.Save(boleta)
            );
        }

        [HttpPut]
        public ActionResult Put([FromBody] Boleta boleta)
        {
            return Ok(
                boletaService.Update(boleta)
            );
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                boletaService.Delete(id)
            );
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(
                boletaService.Get(id)
            );
        }
    }
}