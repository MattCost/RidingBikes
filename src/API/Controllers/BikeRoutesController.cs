using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RidingBikes.API.DatabaseContext;
using RidingBikes.Common.Models;
using RidingBikes.Common.Models.BikeRouteModels;
using RidingBikes.Common.Models.GroupRideModels;

namespace RidingBikes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeRoutesController : ControllerBase
    {
        private readonly RidingBikesContext _context;
        private readonly ILogger<GroupRidesController> _logger;

        public BikeRoutesController(RidingBikesContext context, ILogger<GroupRidesController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BikeRouteViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BikeRouteViewModel>>> GetAllBikeRoutes()
        {
            if (_context.GroupRides == null)
            {
                return NotFound("No Rides in the system");
            }

            var bikeRoutes = await _context.BikeRoutes.Include(route => route.GroupRides).Select(br => BikeRouteViewModel.Create(br)).ToListAsync();
            return bikeRoutes;
            
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BikeRouteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BikeRouteViewModel>> GetBikeRoute([FromRoute] Guid id)
        {
            if (_context.GroupRides == null)
            {
                return NotFound();
            }
            var bikeRoute = await _context.BikeRoutes.Include(x => x.GroupRides).FirstOrDefaultAsync(x => x.Id == id);

            if (bikeRoute == null)
            {
                return NotFound();
            }
            var viewModel = BikeRouteViewModel.Create(bikeRoute);
            _logger.LogDebug("View Model {ViewModel}", viewModel);
            return viewModel;
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBikeRoute([FromRoute] Guid id, [FromBody] BikeRouteUpdateModel updateModel)
        {
            _logger.LogTrace("Searching for Bike Route {BikeRouteId}", id);
            var bikeRide = await _context.BikeRoutes.FindAsync(id);
            if(bikeRide == null)
            {
                _logger.LogWarning("Not Found");
                return NotFound();
            }
            _logger.LogTrace("Checking for update");
            if (bikeRide.Update(updateModel))
            {
                _logger.LogTrace("Saving changes");
                _context.Entry(bikeRide).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception saving entity");
                    return Problem();
                }
            }
            return NoContent();
        }

        //TODO To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<GroupRideModel>> CreateBikeRoute([FromBody] BikeRouteCreateModel createModel)
        {
            if (_context.BikeRoutes == null)
            {
                return Problem("Entity set 'GroupRideContext.BikeRoutes'  is null.");
            }
            var existingBikeRoute = await _context.BikeRoutes.FindAsync(createModel.Id);
            if(existingBikeRoute != null)
            {
                return Conflict($"Bike Route with Id {createModel.Id} already exists. Did you mean to update it?");
            }
            
            var bikeRoute = new BikeRouteModel();
            bikeRoute.Initialize(createModel);

            //TODO wrap in try?            
            _context.BikeRoutes.Add(bikeRoute);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateBikeRoute), new { id = bikeRoute.Id }, bikeRoute);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteBikeRoute(Guid id)
        {
            if (_context.GroupRides == null)
            {
                return NotFound();
            }
            var bikeRoute = await _context.BikeRoutes.Include(br => br.GroupRides).FirstOrDefaultAsync(br => br.Id == id);
            if (bikeRoute == null)
            {
                return NotFound();
            }
            if(bikeRoute.GroupRides.Any())
            {
                return new ConflictObjectResult("Bike Route is used by rides. Can not be deleted");
            }

            _context.BikeRoutes.Remove(bikeRoute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}