using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Planeat_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public ProductController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _repository.Product.GetAllProducts();

                _logger.LogInfo("Returned all products from database");

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllProducts action: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ProductById")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _repository.Product.GetProductById(id);

                if (product.IsObjectNull())
                {
                    _logger.LogError($"Product with id: {id}, hasn't been found in data source");

                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned product with id: {id}");

                    return Ok(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetProductById action: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult CreateProduct([FromBody]Product product)
        {
            try
            {
                if (product.IsObjectNull())
                {
                    _logger.LogError("Product object sent from client is null");
                    return BadRequest("Product object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid product object sent from client");
                    return BadRequest("Invalid product object");
                }

                _repository.Product.CreateProduct(product);

                return CreatedAtRoute("ReciptById", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody]Product product)
        {
            try
            {
                if (product.IsObjectNull())
                {
                    _logger.LogError("Product object sent from client is null");
                    return BadRequest("Product object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid product object sent from client.");
                    return BadRequest("Invalid product object");
                }

                var dbProduct = _repository.Product.GetProductById(id);
                if (dbProduct.IsObjectEmpty())
                {
                    _logger.LogError($"Product with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Product.UpdateProduct(dbProduct, product);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var owner = _repository.Product.GetProductById(id);
                if (owner.IsObjectEmpty())
                {
                    _logger.LogError($"Product with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Product.DeleteProduct(owner);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}