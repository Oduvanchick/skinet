using System;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> repository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
    {
        var specification = new ProductSpecification(specParams);

        return await CreatePagedResult(repository, specification, specParams.PageIndex, specParams.PageSize);

        // var products = await repository.ListAsync(specification);
        // var count = await repository.CountAsync(specification);
        // var pagination = new Pagination<Product>(specParams.PageIndex, specParams.PageSize, count, products);
        // return Ok(pagination);
        // return Ok(await repository.GetProductsAsync(brand, type, sort));
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repository.Add(product);

        if (await repository.SaveAllAsync())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        return BadRequest("Problem creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
        {
            return BadRequest("Cannot update this product");
        }

        repository.Update(product);

        if (await repository.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);

        if (product is null)
        {
            return NotFound();
        }

        repository.Remove(product);

        if (await repository.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting the product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        return Ok(await repository.ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        return Ok(await repository.ListAsync(spec));
    }

    // [HttpGet("brands")]
    // public async Task<ActionResult<IEnumerable<string>>> GetBrands()
    // {
    //     return Ok(await repository.GetBrandsAsync());
    // }

    // [HttpGet("types")]
    // public async Task<ActionResult<IEnumerable<string>>> GetTypes()
    // {
    //     return Ok(await repository.GetTypesAsync());
    // }

    private bool ProductExists(int id)
    {
        return repository.Exists(id);
    }
}
