using Microsoft.AspNetCore.Mvc;
using VShop.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var productsDto = await _productService.GetProducts();

            if(productsDto == null) 
                   return NotFound("products not found");

            return Ok(productsDto);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var productsDto = await _productService.GetProductById(id);

            if (productsDto == null)
                return NotFound("products not found");

            return Ok(productsDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productsDto)
        {
           
            if (productsDto == null)
                return NotFound("products not found");
            await _productService.AddProduct(productsDto);

            return new CreatedAtRouteResult("GetProduct", new { id = productsDto.Id }, productsDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] ProductDTO productsDto)
        {

            if (productsDto == null)
                return BadRequest("Categories not found");
            if (id != productsDto.Id)
                return BadRequest("Categories not found");

            await _productService.UpdateProduct(productsDto);
            return Ok(productsDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var productDto = await _productService.GetProductById(id);

            if (productDto == null)
                return BadRequest("product not found");
            if (id != productDto.Id)
                return BadRequest("product not found");

            await _productService.RemoveProduct(productDto.Id);
            return Ok(productDto);
        }


    }
}
