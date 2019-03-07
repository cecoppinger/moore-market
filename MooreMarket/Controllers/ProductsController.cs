using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MooreMarket.Data;
using MooreMarket.Models;
using Microsoft.AspNetCore.Authorization;


namespace MooreMarket.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private MooreMarketContext _context;
        public ProductsController(MooreMarketContext context)
        {
            _context = context;

        }
        
        //GET Products/
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public IActionResult GetAllProducts()
        {
            IList<Product> allProducts = _context.Products
                .Include(p => p.Category)
                .Include(p => p.User)
                .ToList();


            
            if (allProducts.Count == 0)
            {
                return NoContent();
            }

            return Ok(allProducts);
        }

        //GET Products/{id}
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetProduct(int id)
        {
            Product product = _context.Products.SingleOrDefault(p => p.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //POST Products/Add
        [HttpPost("Add")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Add(Product product)
        {
            ProductCategory ProductCategory = _context.Categories.SingleOrDefault(c => c.Id == product.CategoryId);
            product.Category = ProductCategory;              

            if (product.Name == null)
            {
                return NotFound();
            }

            if (product.Name == "")
            {
                return BadRequest(product);
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        //PUT Products/Edit/{id}
        [HttpPut("Edit/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Edit(int id, Product product)
        {
            product.Id = id;
            ProductCategory ProductCategory = _context.Categories.SingleOrDefault(c => c.Id == product.CategoryId);
            product.Category = ProductCategory;

            if (product.Name == "")
            {
                return BadRequest(product);
            }

            _context.Products.Update(product);

            _context.SaveChanges();

            return Ok(product);
        }

        //DELETE Products/Remove/{id}
        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            Product product = _context.Products.SingleOrDefault(p => p.Id == id);

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }
        [AllowAnonymous]
        [HttpGet("Search")]
        public ActionResult<IEnumerable<Product>> Search(string searchTerm)
        {
            if(searchTerm == null)
            {
                return NoContent();
            }

            var results = _context.Products
                .Include(p => p.User)
                .Where(p => p.Name.Contains(searchTerm) || p.User.Username.Contains(searchTerm))
                .ToList();

            if(results == null)
            {
                return NoContent();
            }

            return results;
        }
    }
}