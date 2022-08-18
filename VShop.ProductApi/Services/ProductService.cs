﻿using AutoMapper;
using VShop.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositories;

namespace VShop.ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = _productRepository;
            _mapper = mapper;
        }



        public async Task AddProduct(ProductDTO productDTO)
        {
            var productsEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.Create(productsEntity);
            productDTO.Id = productsEntity.Id;
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var productsEntity = await _productRepository.GetById(id);
            return _mapper.Map<ProductDTO>(productsEntity);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsEntity = await _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task RemoveProduct(int id)
        {
            var productsEntity =  _productRepository.GetById(id).Result;
            await _productRepository.delete(productsEntity.Id);
        }

        public async Task UpdateProduct(ProductDTO productDTO)
        {
            var productsEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.Update(productsEntity);
        }
    }
}
