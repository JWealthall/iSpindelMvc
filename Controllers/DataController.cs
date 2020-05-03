using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iSpindelMvc.Data;
using iSpindelMvc.Models;
using Microsoft.Extensions.Logging;

namespace iSpindelMvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        //private readonly LogDbContext _context;
        private readonly ILogger<DataController> _logger;
        private readonly LogDb _logDb;

        public DataController(ILogger<DataController> logger, LogDbContext context)
        {
            _logger = logger;
            _logDb = new LogDb(context);
            //_context = context;
        }

        //// GET: api/Data
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Device>>> GetDevice()
        //{
        //    return await _context.Devices.ToListAsync();
        //}

        //// GET: api/Data/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Device>> GetDevice(Guid id)
        //{
        //    var device = await _context.Devices.FindAsync(id);

        //    if (device == null)
        //    {
        //        return NotFound();
        //    }

        //    return device;
        //}

        //// PUT: api/Log/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDevice(Guid id, Device device)
        //{
        //    if (id != device.DeviceId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(device).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DeviceExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Log
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Device>> PostDevice(Device device)
        //{
        //    _context.Devices.Add(device);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDevice", new { id = device.DeviceId }, device);
        //}

        //// DELETE: api/Log/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Device>> DeleteDevice(Guid id)
        //{
        //    var device = await _logDb.DeleteDevice(id);
        //    return device ?? (ActionResult<Device>) NotFound();

        //    //var device = await _context.Devices.FindAsync(id);
        //    //if (device == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //_context.Devices.Remove(device);
        //    //await _context.SaveChangesAsync();

        //    //return device;
        //}

        //private bool DeviceExists(Guid id)
        //{
        //    return _logDb.DeviceExists(id);
        //}

        [HttpPost]
        [Route("Log")]
        public async Task<ActionResult<bool>> Log(SpindelLog data)
        {
            return await _logDb.Log(data);
        }
    }
}
