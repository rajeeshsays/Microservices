using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Caching.Memory;
//using TransportService.DA;
using TransportService.DataAccess;
using TransportService.Model;
using TransportService.DTO;
using AdhiSreeTransportService.Migrations;

namespace TransportService.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportEntryApiController : ControllerBase
    {
        // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
        private readonly IConfiguration? _config;
        private readonly IMemoryCache? _cache;
        private readonly TransportServiceDBContext? _context;
        public TransportEntryApiController(IConfiguration? config, IMemoryCache? cache, TransportServiceDBContext? context)
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
           var transData = await _context.TransportEntry.Where(x => x.ID == id)
                                                  
              .SingleOrDefaultAsync();
          var destinationGroups = await _context.DestinationGroups.Where(x=>x.TransportId == id ).ToListAsync();
              
           if (transData != null && destinationGroups != null)
           {
               foreach (var group in destinationGroups)
               {
                   _context.Entry(group).Reference(dg => dg.Party).Load();
                   transData.DestinationGroups.Add(group);
               }    
           }
 


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
           string cacheKey = $"TransportEntryData_page{page}_pageSize_{pageSize}";
           //if (!_cache.TryGetValue(cacheKey, out List<TransportEntry>? transData))
            var transData = await (from te in _context.TransportEntry.Include(te => te.Vehicle)
                                                    
             join dg in _context.DestinationGroups on te.ID equals dg.TransportId into dgGroup
             from dg in dgGroup.DefaultIfEmpty() 
             select dg).ToListAsync();
               // where te.ID == page && dg.ID == pageSize
              
           var cacheEntryOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromMinutes(10))
               .SetAbsoluteExpiration(TimeSpan.FromHours(1));
      
           if (transData != null)
           {
               return Ok(transData);
           }
           else
           {
               return NotFound();
           }

        }

 
       [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] TransportModel transportData)
        {
           //if (transportData.TransportEntry == null || transportData.DestinationGroup == null)
              if (transportData == null)
               return BadRequest("Invalid transport entry data.");
           try
           {
               if (transportData.TransportEntry != null && transportData.DestinationGroup != null)
               {
                   var transportEntry = new TransportEntry
                   {
                   

                    Date =  transportData.TransportEntry.Date,
                    VehicleId = transportData.TransportEntry.VehicleId,
                    VehicleTypeId = transportData.TransportEntry.VehicleTypeId,
                    DriverId = transportData.TransportEntry.DriverId,
                    Party1 = transportData.TransportEntry.Party1,
                    DestinationGroupId = transportData.TransportEntry.DestinationGroupId,
                    From = transportData.TransportEntry.From,
                    To = transportData.TransportEntry.To,
                    StartKM = transportData.TransportEntry.StartKM,
                    CloseKM = transportData.TransportEntry.CloseKM,
                    Total = transportData.TransportEntry.Total,
                    Loading = transportData.TransportEntry.Loading,
                    Unloading = transportData.TransportEntry.Unloading,
                    LoadingCommision = transportData.TransportEntry.LoadingCommision,
                    UnloadingCommision = transportData.TransportEntry.UnloadingCommision,
                    ReturnDestinationId = transportData.TransportEntry.ReturnDestinationId,
                    HaltDays = transportData.TransportEntry.HaltDays,
                    Rent = transportData.TransportEntry.Rent,
                    Narration = transportData.TransportEntry.Narration,
                    Other = transportData.TransportEntry.Other,
                    AccountId = transportData.TransportEntry.AccountId,
                       // Map properties from DTO to entity
                       // Example: ID = transportData.TransportEntry.ID,
                       // Add other property mappings as needed
                   };
                   await _context.TransportEntry.AddAsync(transportEntry);

                    foreach(short destinationId in transportData.DestinationGroup.DestinationIds)
                    {
                        var destGroup = new DestinationGroup
                        {
                            DestinationId = destinationId,
                            TransportId = transportEntry.ID,
                        };
                        await _context.DestinationGroups.AddAsync(destGroup);
                    }

                  await _context.SaveChangesAsync();
                   return Ok("Transport entry record created successfully.");
               }
               else
               {
                   return BadRequest("Invalid transport entry or destination group data.");
               }
           }
           catch
           {
               return StatusCode(500, "Failed to create the transport entry record.");
           }

        }

        // PUT api/<SalesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] TransportEntry transportEntryForUpdate)
        {
           if (transportEntryForUpdate == null)
               return BadRequest("Invalid transport entry data.");

           // 1. Update the database
           //bool success = await _transportEntryDA.UpdateAsync(transportEntryForUpdate); // returns true if updated
           try
           {
               _context.Entry(transportEntryForUpdate).State = EntityState.Modified;
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
           var transportEntry = await _context.TransportEntry.FindAsync(id);
           if (transportEntry == null)
           {
               return NotFound(); // 404
           }
           _context.Entry(transportEntry).State = EntityState.Deleted;
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

 