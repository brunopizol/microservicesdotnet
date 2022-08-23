using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.DTOs;
using VShop.ProductApi.Roles;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categoriesDto = await _categoryService.GetCategories();
            if(categoriesDto == null)
                return NotFound("Categories not found");
            
            return Ok(categoriesDto);

        }


        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var categoriesDto = await _categoryService.GetCategoriesProducts();
            if (categoriesDto == null)
                return NotFound("Categories not found");

            return Ok(categoriesDto);

        }

        [HttpGet("{id:int}", Name ="GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoriesDto = await _categoryService.GetCategoryById(id);
            if (categoriesDto == null)
                return NotFound("Category not found");

            return Ok(categoriesDto);

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            
            if (categoryDto == null)
                return BadRequest("Categories not found");

            await _categoryService.AddCategory(categoryDto);
            return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.CategoryId}, categoryDto);


        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> put(int id, [FromBody] CategoryDTO categoryDto)
        {

            if (categoryDto == null)
                return BadRequest("Categories not found");
            if (id != categoryDto.CategoryId)
                return BadRequest("Categories not found");

            await _categoryService.UpdateCategory(categoryDto);
            return Ok(categoryDto);


        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<CategoryDTO>> delete(int id)
        {
            var categoryDto = await _categoryService.GetCategoryById(id);

            if (categoryDto == null)
                return BadRequest("product not found");
            if (id != categoryDto.CategoryId)
                return BadRequest("product not found");

            await _categoryService.RemoveCategory(categoryDto.CategoryId);
            return Ok(categoryDto);
        }

    }

}
