using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RebeccaResources.Data;
using RebeccaResources.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebeccaResources.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : Controller
    {
        private readonly IBelleBakeryRepository repository;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IBelleBakeryRepository repository, ILogger<ProductsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(repository.GetAllProducts());
            }
            catch (Exception ex)
            {

                logger.LogError($"Failed to get products {ex}.");
                return BadRequest("Failed to get products");
            }
        }
        
    }
}
