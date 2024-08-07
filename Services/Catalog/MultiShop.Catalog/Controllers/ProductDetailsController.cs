﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Services.ProductDetailDetailServices;


namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailsService;

        public ProductDetailsController(IProductDetailService ProductDetailsService)
        {
            _productDetailsService = ProductDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetailsList()
        {
            var values = await _productDetailsService.GetAllProductDetailAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailsById(string id)
        {
            var values = await _productDetailsService.GetByIdProductDetailAsync(id);
            return Ok(values);
        }
        [HttpGet("GetProductDetailByProductId")]
        public async Task<IActionResult> GetProductDetailByProductId(string id)
        {
            var values = await _productDetailsService.GetByProductIdProductDetailAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductDetails(CreateProductDetailDto createProductDetailsDto)
        {
            await _productDetailsService.CreateProductDetailAsync(createProductDetailsDto);
            return Ok("Ürün Detayı başarıyla eklendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductDetails(string id)
        {
            await _productDetailsService.DeleteProductDetailAsync(id);
            return Ok("Ürün Detayı başarıyla silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductDetails(UpdateProductDetailDto updateProductDetailsDto)
        {
            await _productDetailsService.UpdateProductDetailAsync(updateProductDetailsDto);
            return Ok("Ürün Detayı başarıyla güncellendi");
        }
    }
}
