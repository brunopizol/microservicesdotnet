using VShop.DTOs;

namespace VShop.ProductApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int id);
        Task AddProduct(ProductDTO productDTO);
        Task RemoveProduct(int id);
        Task UpdateProduct(ProductDTO productDTO);
    }
}
