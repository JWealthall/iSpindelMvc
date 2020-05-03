using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iSpindelMvc.Data;
using iSpindelMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iSpindelMvc.Controllers
{
    public class BatchController : Controller
    {
        private readonly LogDb _logDb;
        private readonly ILogger<DataController> _logger;

        public BatchController(ILogger<DataController> logger, LogDbContext context)
        {
            _logger = logger;
            _logDb = new LogDb(context);
        }

        [HttpGet]
        [Route("Batch")]
        public IActionResult Index()
        {
            return View(_logDb.SummaryData(batchesOnly: true));
        }

        [HttpGet]
        [Route("Batch/{batchId:Guid}")]
        public async Task<IActionResult> Index(Guid batchId)
        {
            return View(await _logDb.BatchGetWithSummary(batchId));
        }

        [HttpGet]
        [Route("Batch/Chart/{batchId:Guid}")]
        public async Task<IActionResult> Chart(Guid batchId)
        {
            return View(await _logDb.BatchGetWithSummary(batchId));
        }

        [HttpGet]
        [Route("Batch/Edit/{batchId:Guid}")]
        public IActionResult Edit(Guid batchId)
        {
            return View(_logDb.BatchGet(batchId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Batch/Edit/{batchId:Guid}")]
        public async Task<IActionResult> Edit(Guid batchId, [FromForm] Batch batch)
        {
            if (batchId != batch.BatchId) return NotFound();
            if (!ModelState.IsValid) return View(batch);
            return !await _logDb.BatchSave(batch)
                    ? (IActionResult)NotFound()
                    : RedirectToAction(nameof(Index), new { batchId });
        }

        [HttpGet]
        [Route("Batch/End/{batchId:Guid}")]
        public IActionResult End(Guid batchId)
        {
            return View(_logDb.BatchGet(batchId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Batch/End/{batchId:Guid}")]
        public async Task<IActionResult> End(Guid batchId, [FromForm] Batch batch)
        {
            if (batchId != batch.BatchId) return NotFound();
            if (!ModelState.IsValid) return View(batch);
            return !await _logDb.BatchEnd(batch)
                ? (IActionResult)NotFound()
                : RedirectToAction(nameof(Index), new { batchId });
        }

        [HttpGet]
        [Route("Batch/New/{deviceId:Guid}")]
        public IActionResult New(Guid deviceId)
        {
            var b = new Batch() { DeviceId =  deviceId };
            return View(b);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Batch/New/{deviceId:Guid}")]
        public async Task<IActionResult> New(Guid deviceId, [FromForm] Batch batch)
        {
            if (!ModelState.IsValid) return View(batch);
            return !await _logDb.BatchAdd(batch)
                ? (IActionResult)NotFound()
                : RedirectToAction(nameof(Index), "Device", new { batch.DeviceId });
        }
    }
}