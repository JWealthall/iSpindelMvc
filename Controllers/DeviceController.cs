using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iSpindelMvc.Data;
using iSpindelMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace iSpindelMvc.Controllers
{
    public class DeviceController : Controller
    {
        private readonly LogDb _logDb;
        private readonly ILogger<DataController> _logger;

        public DeviceController(ILogger<DataController> logger, LogDbContext context)
        {
            _logger = logger;
            _logDb = new LogDb(context);
        }

        [HttpGet]
        [Route("Device")]
        public IActionResult Index()
        {
            return View(_logDb.SummaryData(devicesOnly: true));
        }

        [HttpGet]
        [Route("Device/{deviceId:Guid}")]
        public IActionResult Index(Guid deviceId)
        {
            return View(_logDb.SummaryData(deviceId));
        }

        [HttpGet]
        [Route("Device/Edit/{deviceId:Guid}")]
        public IActionResult Edit(Guid deviceId)
        {
            return View(_logDb.DeviceGet(deviceId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Device/Edit/{deviceId:Guid}")]
        public async Task<IActionResult> Edit(Guid deviceId, [FromForm] Device device)
        {
            if (deviceId != device.DeviceId) return NotFound();
            if (!ModelState.IsValid) return View(device);
            return !await _logDb.DeviceSave(device)
               ? (IActionResult)NotFound()
               : RedirectToAction(nameof(Index), new { deviceId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Device/Edit")]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Device/Edit")]
        public async Task<IActionResult> Edit(Device device)
        {
            if (!ModelState.IsValid) return View(device);
            return !await _logDb.DeviceSave(device)
                ? (IActionResult)NotFound()
                : RedirectToAction(nameof(Index), new { device.DeviceId });
        }

    }
}