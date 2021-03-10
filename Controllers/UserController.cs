using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using demoapi.Models;

namespace demoapi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly demoapiContext _context;
        public UserController (demoapiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var results = await _context.Users.AsNoTracking().ToListAsync();
            return Ok(results);

        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<User>> GetById(int? id)
        {
            if(id == null){
                return NotFound();
            }
            var result = await _context.Users.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if(result == null){
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> UpdateUser (int id , User user)
        {
            if(id != user.Id){
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult> DeleteUserById (int id)
        {
            var result = await _context.Users.FindAsync(id);
            if(result == null){
                return NotFound();
            }
            _context.Users.Remove(result);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { Message="Delete Success !!"});
        }

    }
}