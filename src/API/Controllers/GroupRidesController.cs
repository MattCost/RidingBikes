using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RidingBikes.API.DatabaseContext;
using RidingBikes.Common.Models;

namespace RidingBikes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupRidesController : ControllerBase
    {
        private readonly RidingBikesContext _context;

        public GroupRidesController(RidingBikesContext context)
        {
            _context = context;
        }

        // GET: api/GroupRides
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupRide>>> GetGroupRides()
        {
          if (_context.GroupRides == null)
          {
              return NotFound();
          }
            return await _context.GroupRides.Include(ride => ride.BikeRoute).ToListAsync();
            // return await _context.GroupRides.ToListAsync();
        }

        // GET: api/GroupRides/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GroupRide>> GetGroupRide([FromRoute] Guid id)
        {
          if (_context.GroupRides == null)
          {
              return NotFound();
          }
            var groupRide = await _context.GroupRides.FindAsync(id);

            if (groupRide == null)
            {
                return NotFound();
            }

            return groupRide;
        }

        // PUT: api/GroupRides/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutGroupRide([FromRoute] Guid id, [FromBody] GroupRide groupRide)
        {
            if (id != groupRide.Id)
            {
                return BadRequest();
            }

            _context.Entry(groupRide).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupRideExists(id))
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

        // POST: api/GroupRides
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupRide>> PostGroupRide([FromBody] GroupRide groupRide)
        {
          if (_context.GroupRides == null)
          {
              return Problem("Entity set 'GroupRideContext.GroupRides'  is null.");
          }
            _context.GroupRides.Add(groupRide);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupRide", new { id = groupRide.Id }, groupRide);
        }

        // DELETE: api/GroupRides/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGroupRide(Guid id)
        {
            if (_context.GroupRides == null)
            {
                return NotFound();
            }
            var groupRide = await _context.GroupRides.FindAsync(id);
            if (groupRide == null)
            {
                return NotFound();
            }

            _context.GroupRides.Remove(groupRide);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupRideExists(Guid id)
        {
            return (_context.GroupRides?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}