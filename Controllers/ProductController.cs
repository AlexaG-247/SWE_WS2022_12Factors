
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Microservice.Data;
using Product.Microservice.Model;

namespace Product.Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        private readonly IApplicationDbContext  _context;

		private readonly ILogger<ProductController> _logger;

        public ProductController(IApplicationDbContext context, ILogger<ProductController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        //HTTP_GET
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.ProductItems.ToListAsync();

            if (products is null){
				_logger.LogInformation("{StatusCode} - No products found.", (int)HttpStatusCode.NotFound);
				return NotFound();
			}

			_logger.LogInformation("{StatusCode} - Products returned.", (int)HttpStatusCode.OK);
            return Ok(products);
        }

        //HTTP_GET
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductItem), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.ProductItems.FindAsync(id);

            if (product is null){
				_logger.LogInformation("{StatusCode} - Product with {id} not found.", (int)HttpStatusCode.NotFound ,id);
				return NotFound();
			}

			_logger.LogInformation("{StatusCode} - Product with {id} returned.", (int)HttpStatusCode.OK, product.Id);
            return Ok(product);
        }

        //HTTP_POST
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(ProductItem product)
        {
            _context.ProductItems.Add(product);

            await _context.SaveChanges();

			_logger.LogInformation("{StatusCode} - Product with {id} added.", (int)HttpStatusCode.OK, product.Id);
            return Ok(product.Id);
        }

        //HTTP_PUT
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(int id, ProductItem productData)
        {
            var product = await _context.ProductItems.FindAsync(id);

            if (product is null){
				_logger.LogInformation("{StatusCode} - Product with {id} not found.", (int)HttpStatusCode.NotFound, id);
				return NotFound();
			}
            else
            {
                product.Name = productData.Name;
                product.Description = productData.Description;
                product.Price = productData.Price;

                await _context.SaveChanges();

				_logger.LogInformation("{StatusCode} - Product with {id} updated.", (int)HttpStatusCode.OK, product.Id);
                return Ok(product.Id);
            }
        }

        //HTTP_DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.ProductItems.FindAsync(id);

            if (product is null){
				_logger.LogInformation("{StatusCode} - Product with {id} not found.", (int)HttpStatusCode.NotFound ,id);
				return NotFound();
			}

            _context.ProductItems.Remove(product);
            await _context.SaveChanges();

			_logger.LogInformation("{StatusCode} - Product with {id} deleted.", (int)HttpStatusCode.OK ,product.Id);
            return Ok(product.Id);
        }
    }
}
