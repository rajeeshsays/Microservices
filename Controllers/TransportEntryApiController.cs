using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Caching.Memory;
//using TransportService.DA;
using TransportService.DataAccess;
using TransportService.Model;
using TransportService.DTO;
using AdhiSreeTransportService.Migrations;
using System.Linq;

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
          var destinationGroups = await _context.DestinationGroups.Where(x=>x.TransportId == id )
          .Select(x=>x.DestinationId        
          
          ).ToArrayAsync<short>();
        

           var transportEntryDto = new TransportEntryDto
            {   
            ID = transData.ID,
            Date = transData.Date,
            VehicleId = transData.VehicleId,
            VehicleTypeId = transData.VehicleTypeId,
            DriverId = transData.DriverId,
            Party1 = transData.Party1,           
            DestinationGroupId = transData.DestinationGroupId,
            DestinationGroups = destinationGroups,
            From = transData.From,
            To = transData.To,
            StartKM = transData.StartKM,
            CloseKM = transData.CloseKM,
            Total = transData.Total,
            Loading = transData.Loading,
            Unloading = transData.Unloading,
            LoadingCommision = transData.LoadingCommision,
            UnloadingCommision = transData.UnloadingCommision,
            //public string ReturnDestination { get; set; }
            ReturnDestinationId = transData.ReturnDestinationId,
            HaltDays = transData.HaltDays,
            Rent = transData.Rent,
            Narration = transData.Narration,        
            Other = transData.Other,
            AccountId = transData.AccountId,
            };


           var cacheEntryOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromMinutes(10))
               .SetAbsoluteExpiration(TimeSpan.FromHours(1));
           var x = 1;
           if (transportEntryDto != null)
           {
               return Ok(transportEntryDto);
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
           var transData = await (
            from te in _context.TransportEntry
            .Include(te => te.Vehicle)

            join dg in _context.DestinationGroups
            on te.ID equals dg.TransportId into dgGroup

            from dg in dgGroup.DefaultIfEmpty()
            select te
            ).Distinct().ToListAsync();
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
        if (transportData?.TransportEntry == null ||
        transportData.DestinationGroup?.DestinationIds == null ||
        !transportData.DestinationGroup.DestinationIds.Any())
        {
        return BadRequest("Invalid transport entry or destination group data." + " Please check the input data."+transportData?.TransportEntry);
        }

        try
        {
        // 1️⃣ Create TransportEntry (PARENT)
        var transportEntry = new TransportEntry
        {
            Date = transportData.TransportEntry.Date,
            VehicleId = transportData.TransportEntry.VehicleId,
            VehicleTypeId = transportData.TransportEntry.VehicleTypeId,
            DriverId = transportData.TransportEntry.DriverId,
            Party1 = transportData.TransportEntry.Party1,
            From = transportData.TransportEntry.From,
            To = transportData.TransportEntry.To,
            StartKM = transportData.TransportEntry.StartKM,
            CloseKM = transportData.TransportEntry.CloseKM,
            Total = transportData.TransportEntry.Total,
            Loading = transportData.TransportEntry.Loading,
            Unloading = transportData.TransportEntry.Unloading,
            LoadingCommision = transportData.TransportEntry.LoadingCommision,
            UnloadingCommision = transportData.TransportEntry.UnloadingCommision,
            ReturnDestinationId = transportData.TransportEntry.ReturnDestinationId == 0 ? 1 : transportData.TransportEntry.ReturnDestinationId,
            HaltDays = transportData.TransportEntry.HaltDays,
            Rent = transportData.TransportEntry.Rent,
            Narration = transportData.TransportEntry.Narration,
            Other = transportData.TransportEntry.Other,
            AccountId = transportData.TransportEntry.AccountId,
            DestinationGroupId  = 0
        };


        await _context.TransportEntry.AddAsync(transportEntry);

        // 2️⃣ Save FIRST to generate TransportEntry.ID
        await _context.SaveChangesAsync();

        // 3️⃣ Validate DestinationIds against Party table
        var destinationIds = transportData.DestinationGroup.DestinationIds;

        // 4️⃣ Create DestinationGroups (CHILD)
        var destinationGroups = destinationIds.Select(destinationId =>
            new DestinationGroup
            {
                TransportId = transportEntry.ID, // NOW VALID
                DestinationId = (short)destinationId
            }).ToList();

        await _context.DestinationGroups.AddRangeAsync(destinationGroups);
        transportEntry.DestinationGroupId = destinationGroups.First().TransportId;
        // 5️⃣ Save children
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Transport entry record created successfully.",
            TransportId = transportEntry.ID
        });
    }
    catch (Exception ex)
    {
        // OPTIONAL: log ex
        return StatusCode(500, "Failed to create the transport entry record.");
    }
}


        // PUT api/<SalesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] TransportModel model)
        {
            if (model?.TransportEntry == null ||
                model.DestinationGroup?.DestinationIds == null ||
                !model.DestinationGroup.DestinationIds.Any())
        {   
        return BadRequest("Invalid transport entry or destination group data.");
        }

    // Fetch existing TransportEntry
    var transportEntry = await _context.TransportEntry
        .Include(t => t.DestinationGroups)
        .FirstOrDefaultAsync(t => t.ID == id);

    if (transportEntry == null)
    {
        return NotFound($"Transport entry with ID {id} not found.");
    }

    //await using var transaction = await _context.Database.BeginTransactionAsync();

    try
    {
        // 1️⃣ Update parent entity
        var dto = model.TransportEntry;

        transportEntry.Date = dto.Date;
        transportEntry.VehicleId = dto.VehicleId;
        transportEntry.VehicleTypeId = dto.VehicleTypeId;
        transportEntry.DriverId = dto.DriverId;
        transportEntry.Party1 = dto.Party1;
        transportEntry.From = dto.From;
        transportEntry.To = dto.To;
        transportEntry.StartKM = dto.StartKM;
        transportEntry.CloseKM = dto.CloseKM;
        transportEntry.Total = dto.Total;
        transportEntry.Loading = dto.Loading;
        transportEntry.Unloading = dto.Unloading;
        transportEntry.LoadingCommision = dto.LoadingCommision;
        transportEntry.UnloadingCommision = dto.UnloadingCommision;
        transportEntry.ReturnDestinationId = dto.ReturnDestinationId == 0 ? 1 : dto.ReturnDestinationId;
        transportEntry.HaltDays = dto.HaltDays;
        transportEntry.Rent = dto.Rent;
        transportEntry.Narration = dto.Narration;
        transportEntry.Other = dto.Other;
        transportEntry.AccountId = dto.AccountId;

        // 2️⃣ Remove existing DestinationGroups
        _context.DestinationGroups.RemoveRange(transportEntry.DestinationGroups);

        // 3️⃣ Add new DestinationGroups
        transportEntry.DestinationGroups = model.DestinationGroup.DestinationIds
            .Select(destinationId => new DestinationGroup
            {
                TransportId = transportEntry.ID,
                DestinationId = (short)destinationId
            })
            .ToList();

        // 4️⃣ Save changes
        await _context.SaveChangesAsync();
        //await transaction.CommitAsync();

        return Ok(new
        {
            Message = "Transport entry updated successfully.",
            TransportId = transportEntry.ID
        });
    }
    catch (Exception Ex)
    {
        //await transaction.RollbackAsync();
        return StatusCode(500, "Failed to update the transport entry record.");
    }
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

 