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
    public class PartyApiController : ControllerBase
    {
        // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
        private readonly IConfiguration? _config;
        private readonly IMemoryCache? _cache;
        private readonly TransportServiceDBContext? _context;
        public PartyApiController(IConfiguration? config, IMemoryCache? cache, TransportServiceDBContext? context)
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
           var transData = await _context.Party.Where(x => x.ID == id)
                                                 
              .SingleOrDefaultAsync();
         

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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int page, int pageSize)
        {
           if (_context == null)
               return StatusCode(500, "Database context is not available.");
           string cacheKey = $"Party_page{page}_pageSize_{pageSize}";
           //if (!_cache.TryGetValue(cacheKey, out List<TransportEntry>? transData))
            var parties = await (from te in _context.Party
                                                                         
              select te).ToListAsync();
               // where te.ID == page && dg.ID == pageSize
              
           var cacheEntryOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromMinutes(10))
               .SetAbsoluteExpiration(TimeSpan.FromHours(1));

           if (parties != null)
           {
               return Ok(parties);
           }
           else
           {
               return NotFound();
           }

        }

 
       [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Party partyModel)
        {
           if (partyModel   == null)
               return BadRequest("Invalid party data.");
           try
           {
               await _context.Party.AddAsync(partyModel);  
               await _context.SaveChangesAsync();
               return Ok("Party record created successfully.");
           }
           catch
           {
               return StatusCode(500, "Failed to create the party record.");
           }

        }

        // PUT api/<SalesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Party partyEntryForUpdate)
        {
           if (partyEntryForUpdate == null)
               return BadRequest("Invalid party data.");
           // 1. Update the database
           //bool success = await _transportEntryDA.UpdateAsync(transportEntryForUpdate); // returns true if updated
           try
           {
               _context.Entry(partyEntryForUpdate).State = EntityState.Modified;
               await _context.SaveChangesAsync();
               return Ok("Party record updated and cache cleared.");
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
           var party = await _context.Party.FindAsync(id);
           if (party == null)
           {
               return NotFound(); // 404
           }
           _context.Entry(party).State = EntityState.Deleted;
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

 