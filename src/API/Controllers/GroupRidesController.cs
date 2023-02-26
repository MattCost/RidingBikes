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
        private readonly ILogger<GroupRidesController> _logger;

        public GroupRidesController(RidingBikesContext context, ILogger<GroupRidesController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GroupRideViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GroupRideViewModel>>> GetAllGroupRides()
        {
            if (_context.GroupRides == null)
            {
                return NotFound("No Rides in the system");
            }
            var groupRides = await _context.GroupRides.Include(ride => ride.BikeRoute).Select(gr => GroupRideViewModel.Create(gr)).ToListAsync();

            return groupRides;
            
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GroupRideViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupRideViewModel>> GetGroupRide([FromRoute] Guid id)
        {
            if (_context.GroupRides == null)
            {
                return NotFound("No Rides in the system");
            }
            var groupRide = await _context.GroupRides.Include(ride => ride.BikeRoute).FirstOrDefaultAsync(gr => gr.Id == id);

            if (groupRide == null)
            {
                return NotFound($"Ride Id {id} not found");
            }
            var viewModel = GroupRideViewModel.Create(groupRide);
            _logger.LogDebug("View Model {ViewModel}", viewModel);
            return viewModel;
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGroupRide([FromRoute] Guid id, [FromBody] GroupRideUpdateModel groupRideUpdateModel)
        {
            _logger.LogTrace("Searching for GroupRide {GroupRideId}", id);
            var groupRide = await _context.GroupRides.FindAsync(id);
            if(groupRide == null)
            {
                _logger.LogWarning("Ride Id {GroupRideId} not found", id);
                return NotFound($"Ride Id {id} not found");
            }
            _logger.LogTrace("Checking for update");
            if (groupRide.Update(groupRideUpdateModel))
            {
                _logger.LogTrace("Saving changes");
                _context.Entry(groupRide).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving changes");
                    return Problem();
                }
            }

            return NoContent();
        }

        //TODO To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<GroupRideViewModel>> CreateGroupRide([FromBody] GroupRideCreateModel createModel)
        {
            if (_context.GroupRides == null)
            {
                return Problem("Entity set 'GroupRideContext.GroupRides'  is null.");
            }
            var existingRide = await _context.GroupRides.FindAsync(createModel.Id);
            if(existingRide != null)
            {
                return Conflict($"Group Ride with Id {createModel.Id} already exists. Did you mean to update it?");
            }

            var bikeRoute = await _context.BikeRoutes.FindAsync(createModel.BikeRouteId);
            if(bikeRoute == null)
            {
                return new BadRequestObjectResult($"Bike Route with Id {createModel.BikeRouteId} does not exist. Unable to reference for a group ride");
            }

            var groupRide = GroupRideModel.Create(createModel);
            groupRide.BikeRoute = bikeRoute;

            //TODO wrap in try?            
            _context.GroupRides.Add(groupRide);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateGroupRide), new { id = groupRide.Id }, GroupRideViewModel.Create(groupRide));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGroupRide(Guid id)
        {
            if (_context.GroupRides == null)
            {
                return NotFound("No Rides in the system");
            }
            var groupRide = await _context.GroupRides.FindAsync(id);
            if (groupRide == null)
            {
                return NotFound($"Ride Id {id} not found");
            }

            _context.GroupRides.Remove(groupRide);
            //TODO try catch?
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}