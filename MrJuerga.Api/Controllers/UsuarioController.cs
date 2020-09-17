using MrJuerga.Entity;
using MrJuerga.Service;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Collections.Generic;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using System.Threading.Tasks;
using MrJuerga.Repository.dbcontext;

namespace MrJuerga.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController: ControllerBase
    {
        private IUsuarioService usuarioService;

        private ApplicationDbContext context;
      

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                usuarioService.GetAll()
            );
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(
                usuarioService.Get(id)
            );
        }

        [HttpGet("fetchbyname/{name}")]
        public ActionResult FetchUsuariobyName(string name)
        {
            return Ok(
                usuarioService.FetchUsuariobyName(name)
            );
        }

        [HttpGet("GetExcel")]
        public FileContentResult GetExcel()
        {
            byte[] datos = usuarioService.GetExcel();

            return File(datos, "application/vnd.ms-excel", "usuarios.xlsx");
        }

        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            return Ok(
                usuarioService.Save(usuario)
            );
        }

        [HttpPut]
        public ActionResult Put([FromBody] Usuario usuario)
        {
            return Ok(
                usuarioService.Update(usuario)
            );
        }

        [HttpPut("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                usuarioService.Delete(id)
            );
        }
            }
}