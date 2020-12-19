using MrJuerga.Entity;
using MrJuerga.Service;
using Microsoft.AspNetCore.Mvc;

namespace MrJuerga.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private ICategoriaService categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            this.categoriaService = categoriaService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                categoriaService.GetAll()
            );
        }       
       

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(
                categoriaService.Get(id)
            );
        }
    }
}