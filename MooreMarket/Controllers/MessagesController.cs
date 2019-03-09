using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MooreMarket.Data;
using MooreMarket.Models;

namespace MooreMarket.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private MooreMarketContext _context;
        public MessagesController(MooreMarketContext context)
        {
            _context = context;

        }
        
        //GET Messages/
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public IActionResult GetAllMessages()
        {
            IList<Message> allMessages = _context.Messages
                .Include(u => u.User)
                .Where(m => m.EndDate >= DateTime.Today && m.StartDate <= DateTime.Today )
                .ToList();


            
            if (allMessages.Count == 0)
            {
                return NoContent();
            }

            return Ok(allMessages);
        }

        //GET Messages/{id}
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetMessage(int id)
        {
            Message message = _context.Messages.SingleOrDefault(m => m.Id == id);

            if(message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        [AllowAnonymous]
        [HttpGet("vendor/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetVendorMessages(int id)
        {
            IList<Message> messages = _context.Messages.Where(m => m.UserId == id).ToList();

            if(messages == null)
            {
                return NotFound();
            }

            return Ok(messages);
        }

        //POST Messages/Add
        [HttpPost("Add")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Add(Message message)
        {

            if (message.Title == null)
            {
                return NotFound();
            }

            if (message.Title == "")
            {
                return BadRequest(message);
            }

            _context.Messages.Add(message);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }

        //PUT Messages/Edit/{id}
        [HttpPut("Edit/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Edit(int id, Message message)
        {
            message.Id = id;

            if (message.Title == "")
            {
                return BadRequest(message);
            }

            _context.Messages.Update(message);

            _context.SaveChanges();

            return Ok(message);
        }

        //DELETE Messages/Remove/{id}
        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            Message message = _context.Messages.SingleOrDefault(m => m.Id == id);

            _context.Messages.Remove(message);
            _context.SaveChanges();

            return NoContent();
        }
    }
}