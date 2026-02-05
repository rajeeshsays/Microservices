using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Caching.Memory;
//using TransportService.DA;
using TransportService.DataAccess;
using TransportService.Model;
using TransportService.DTO;

namespace TransportService.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityApiController : ControllerBase
    {
        // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
        private readonly IConfiguration? _config;
        private readonly IMemoryCache? _cache;
        private readonly TransportServiceDBContext? _context;

        public UtilityApiController(IConfiguration? config, IMemoryCache? cache, TransportServiceDBContext? context)
        {
            _config = config;
            _cache = cache;
            _context = context;
        }
        
        [HttpGet("getstates")]
        public async Task<IActionResult> GetStatesAsync()

        {
           if (_context == null)
               return StatusCode(500, "Database context is not available.");
         
           //if (!_cache.TryGetValue(cacheKey, out List<TransportEntry>? transData))
            var states = await (from d in _context.State
              
              where d.IsActive == true
                                                                         
              select new
              {
                    d.ID,
                    d.Name,
                    d.Code
              })
              .ToListAsync();

               // where te.ID == page && dg.ID == pageSize        
      
           if (states != null)
           {
               return Ok(states);
           }
           else
           {
               return NotFound();
           }

        }

                
        [HttpGet("getdistricts/{stateId}")]
        public async Task<IActionResult> GetDistrictsAsync(string stateId)
        {
            if (_context == null)
                return StatusCode(500, "Database context is not available.");

            var districts = await (from d in _context.District
                                    where d.StateId == stateId
                                    select new
                                    {
                                        d.ID,
                                        d.Name,
                                        d.StateId
                                    })
                                    .ToListAsync();

            if (districts != null && districts.Count > 0)
            {
                return Ok(districts);
            }
            else
            {
                return NotFound();
            }
        }
    }
}

 