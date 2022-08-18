using AutoMapper;
using VShop.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories;

namespace VShop.ProductApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddCategory(CategoryDTO categoryDTO)
        {
            var CategoryEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Create(CategoryEntity);
            categoryDTO.CategoryId = CategoryEntity.CategoryId;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var CategoriesEntity = await _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(CategoriesEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            var CategoriesEntity = await _categoryRepository.GetCategoriesProducts();
            return _mapper.Map<IEnumerable<CategoryDTO>>(CategoriesEntity);
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var CategoriesEntity = await _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDTO>(CategoriesEntity);
        }

        public async Task RemoveCategory(int id)
        {
            var CategoryEntity = _categoryRepository.GetById(id).Result;
            await _categoryRepository.Delete(CategoryEntity.CategoryId);
        }


        public async Task UpdateCategory(CategoryDTO categoryDTO)
        {
            var CategoryEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Update(CategoryEntity);
        }
    }
}
