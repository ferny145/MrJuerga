using MrJuerga.Entity;
using MrJuerga.Service;
using Microsoft.AspNetCore.Mvc;

namespace MrJuerga.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoPaqueteController : ControllerBase
    {
        private IProductoPaqueteService productopaqueteService;

        public ProductoPaqueteController(IProductoPaqueteService productopaqueteService)
        {
            this.productopaqueteService = productopaqueteService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(
                productopaqueteService.GetAll()
            );
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductoPaquete productoPaquete)
        {
            return Ok(
                productopaqueteService.Save(productoPaquete)
            );
        }

        [HttpPut]
        public ActionResult Put([FromBody] ProductoPaquete productoPaquete)
        {
            return Ok(
                productopaqueteService.Update(productoPaquete)
            );
        }

        [HttpPut("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(
                productopaqueteService.Delete(id)
            );
        }
    }
}