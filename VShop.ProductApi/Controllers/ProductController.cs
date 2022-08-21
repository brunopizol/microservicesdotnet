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
            var produtosDto = await _productService.GetProducts();
            if (produtosDto == null)
            {
                return NotFound("Products not found");
            }
            return Ok(produtosDto);
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

        [HttpPut()]
        public async Task<ActionResult> Put([FromBody] ProductDTO productsDto)
        {

            if (productsDto == null)
                return BadRequest("data invalid");
            

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
