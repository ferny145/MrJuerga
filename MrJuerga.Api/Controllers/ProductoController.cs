using MrJuerga.Entity;
using MrJuerga.Service;
using Microsoft.AspNetCore.Mvc;

namespace MrJuerga.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
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

        [HttpGet("fetchbyname/{name}")]
        public ActionResult FetchProductobyName(string name)
        {
            return Ok(
                productoService.FetchProductobyName(name)
            );
        }

        [HttpGet("fetchbycategory/{id}")]
        public ActionResult FetchProductobyCategory(int id)
        {
            return Ok(
                productoService.FetchProductobyCategory(id)
            );
        }

        [HttpGet("GetImage/{name}")]
        public ActionResult GetImage(string name)
        {
           
            return Ok( productoService.GetImage(name));
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

        [HttpPut("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                productoService.Delete(id)
            );
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(
                productoService.Get(id)
            );
        }
    }
}