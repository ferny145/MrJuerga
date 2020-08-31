using MrJuerga.Entity;
using MrJuerga.Service;
using Microsoft.AspNetCore.Mvc;

namespace MrJuerga.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController: ControllerBase
    {
        private IProductoService productoService;

        public ProductoController(IProductoService productoService)
        {
            this.productoService = productoService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                productoService.GetAll()
            );
        }

        [HttpPost]
        public ActionResult Post([FromBody] Producto producto)
        {
            return Ok(
                productoService.Save(producto)
            );
        }

        [HttpPut]
        public ActionResult Put([FromBody] Producto producto)
        {
            return Ok(
                productoService.Update(producto)
            );
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                productoService.Delete(id)
            );
        }
    }
}