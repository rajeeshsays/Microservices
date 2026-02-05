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
    public class DriverApiController : ControllerBase
    {
        // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
        private readonly IConfiguration? _config;
        private readonly IMemoryCache? _cache;
        private readonly TransportServiceDBContext? _context;
        public DriverApiController(IConfiguration? config, IMemoryCache? cache, TransportServiceDBContext? context)
        {
            _config = config;
            _cache = cache;
            _context = context;
        }
        
 
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok("Transport Entry API Controller");
        }


        //GET: api/<SalesController>
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
          // var transData = await _transportEntryDA.GetAsync(id); // Call DB
           //Set cache options
           var transData = await _context.Driver.Where(x => x.ID == id)
                                                 
              .SingleOrDefaultAsync();
          var destinationGroups = await _context.DestinationGroups.Where(x=>x.TransportId == id ).ToListAsync();
              

 


           var cacheEntryOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromMinutes(10))
               .SetAbsoluteExpiration(TimeSpan.FromHours(1));
           var x = 1;
           if (transData != null)
           {
               return Ok(transData);
           }
           else
           {
               return NotFound();
           }
        }

         [HttpGet("getdriver")]
        public async Task<IActionResult> GetDriverAsync()

        {
           if (_context == null)
               return StatusCode(500, "Database context is not available.");
         
           //if (!_cache.TryGetValue(cacheKey, out List<TransportEntry>? transData))
            var driver = await (from d in _context.Driver
                                                                         
              select new
              {
                    d.ID,
                    d.Name,
              })
              .ToListAsync();
               // where te.ID == page && dg.ID == pageSize
              
      
           if (driver != null)
           {
               return Ok(driver);
           }
           else
           {
               return NotFound();
           }

        }


        [HttpGet("getall/{page}/{pageSize}")]
        public async Task<IActionResult> GetAllAsync(int page, int pageSize)
        {
           if (_context == null)
               return StatusCode(500, "Database context is not available.");
           string cacheKey = $"TransportEntryData_page{page}_pageSize_{pageSize}";
           //if (!_cache.TryGetValue(cacheKey, out List<TransportEntry>? transData))
            var driver = await (from te in _context.Driver
                                                                         
              select te).ToListAsync();
               // where te.ID == page && dg.ID == pageSize
              
           var cacheEntryOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromMinutes(10))
               .SetAbsoluteExpiration(TimeSpan.FromHours(1));
      
           if (driver != null)
           {
               return Ok(driver);
           }
           else
           {
               return NotFound();
           }

        }

 
       [HttpPost("create")]

        public async Task<IActionResult> CreateAsync([FromBody] Driver driverModel)
        {
           if (driverModel   == null)
               return BadRequest("Invalid driver data.");
           try
           {
               await _context.Driver.AddAsync(driverModel);  
               await _context.SaveChangesAsync();
               return Ok("Driver record created successfully.");
           }
           catch
           {
               return StatusCode(500, "Failed to create the driver record.");
           }

        }

        // PUT api/<SalesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Driver driverEntryForUpdate)
        {
           if (driverEntryForUpdate == null)
               return BadRequest("Invalid driver data.");

           // 1. Update the database
           //bool success = await _transportEntryDA.UpdateAsync(transportEntryForUpdate); // returns true if updated
           try
           {
               _context.Entry(driverEntryForUpdate).State = EntityState.Modified;
               await _context.SaveChangesAsync();
               return Ok("Transport entry record updated and cache cleared.");
           }
           catch (DbUpdateConcurrencyException)
           {
               return StatusCode(StatusCodes.Status500InternalServerError, "A concurrency error occurred");

           }
           catch (DbUpdateException)
           {
               return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update transport entry.");

           }
            // 2. Invalidate relevant cache entries
           //InvalidateSalesCache();
        }


        //DELETE api/<SalesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
           var driver = await _context.Driver.FindAsync(id);
           if (driver == null)
           {
               return NotFound(); // 404
           }
           _context.Entry(driver).State = EntityState.Deleted;
           await _context.SaveChangesAsync();
           return NoContent(); // 204
        }

        // [HttpGet("clearcache")]
        // public void InvalidateSalesCache(int pageSize, int maxPages)
        // {
        //    for (int i = 1; i <= maxPages; i++)
        //    {
        //        string key = $"TransportEntryData_page{i}_pageSize_{pageSize}";
        //        _cache.Remove(key);
        //    }
        // }

    }
}

 