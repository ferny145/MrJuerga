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
    }
}