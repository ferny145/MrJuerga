using MrJuerga.Entity;
using MrJuerga.Service;
using Microsoft.AspNetCore.Mvc;

namespace MrJuerga.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {

        private IPacienteService pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            this.pacienteService = pacienteService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                pacienteService.GetAll()
            );
        }

        [HttpPost]
        public ActionResult Post([FromBody] Paciente paciente)
        {
            return Ok(
                pacienteService.Save(paciente)
            );
        }

        [HttpPut]
        public ActionResult Put([FromBody] Paciente paciente)
        {
            return Ok(
                pacienteService.Update(paciente)
            );
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                pacienteService.Delete(id)
            );
        }

    }
}