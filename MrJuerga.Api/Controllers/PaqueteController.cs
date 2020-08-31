using MrJuerga.Entity;
using MrJuerga.Service;
using Microsoft.AspNetCore.Mvc;

namespace MrJuerga.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaqueteController: ControllerBase
    {
        private IPaqueteService paqueteService;

        public PaqueteController(IPaqueteService paqueteService)
        {
            this.paqueteService = paqueteService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                paqueteService.GetAll()
            );
        }

        [HttpPost]
        public ActionResult Post([FromBody] Paquete paquete)
        {
            return Ok(
                paqueteService.Save(paquete)
            );
        }

        [HttpPut]
        public ActionResult Put([FromBody] Paquete paquete)
        {
            return Ok(
                paqueteService.Update(paquete)
            );
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                paqueteService.Delete(id)
            );
        }
    }
}