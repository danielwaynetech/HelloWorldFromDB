using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloWorldFromDB;

namespace HelloWorldFromDB.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly HelloWorldRepositoryContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MessagesController(HelloWorldRepositoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Messages>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Messages>> GetMessages(string id)
        {
            var messages = await _context.Messages.FindAsync(id);

            if (messages == null)
            {
                return NotFound();
            }

            return messages;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <param name="messages"></param>
       /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessages(string id, Messages messages)
        {
            if (id != messages.Author)
            {
                return BadRequest();
            }

            _context.Entry(messages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessagesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Messages>> PostMessages(Messages messages)
        {
            _context.Messages.Add(messages);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MessagesExists(messages.Author))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMessages", new { id = messages.Author }, messages);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Messages>> DeleteMessages(string id)
        {
            var messages = await _context.Messages.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(messages);
            await _context.SaveChangesAsync();

            return messages;
        }

        private bool MessagesExists(string id)
        {
            return _context.Messages.Any(e => e.Author == id);
        }
    }
}
