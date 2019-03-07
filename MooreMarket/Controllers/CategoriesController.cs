using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MooreMarket.Data;
using MooreMarket.Models;
using Microsoft.AspNetCore.Authorization;

namespace MooreMarket.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private MooreMarketContext _context;
        public CategoriesController(MooreMarketContext context)
        {
            _context = context;

        }
        
        //GET api/Categories/
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public IActionResult GetAllCategories()
        {
            IList<ProductCategory> allCategories = _context.Categories.ToList();

            if (allCategories.Count == 0)
            {
                return NoContent();
            }

            return Ok(allCategories);
        }

        //GET api/Categories/{id}
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetCategory(int id)
        {
            ProductCategory category = _context.Categories.Single(c => c.Id == id);

            if(category.Name == "")
            {
                return NotFound();
            }

            return Ok(category);
        }

        //POST api/Categories/Add
        [HttpPost("Add")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Add(ProductCategory category)
        {
            //Validate category data
            if (category.Name == "")
            {
                return BadRequest(category);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        //PUT Categories/Edit/{id}
        [HttpPut("Edit/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Edit(int id, ProductCategory category)
        {
            ProductCategory oldCategory = _context.Categories.SingleOrDefault(c => c.Id == id);

            if (oldCategory == null)
            {
                return NotFound();

            }

            if (category.Name == "")
            {
                return BadRequest(category);
            }

            
            oldCategory.Name = category.Name;

            _context.SaveChanges();

            return Ok(category);
        }

        //DELETE Categories/Remove/{id}
        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            ProductCategory category = _context.Categories.SingleOrDefault(c => c.Id == id);
            _context.Categories.Remove(category);

            _context.SaveChanges();

            return Ok();
        }
    }
}