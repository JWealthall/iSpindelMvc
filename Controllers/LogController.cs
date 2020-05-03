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
    public class LogController : Controller
    {
        private readonly LogDb _logDb;
        private readonly ILogger<DataController> _logger;

        public LogController(ILogger<DataController> logger, LogDbContext context)
        {
            _logger = logger;
            _logDb = new LogDb(context);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        [Route("Log/Delete/{logId:Guid}")]
        public async Task<IActionResult> Delete(Guid logId)
        {
            return View(await _logDb.LogGet(logId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Log/Delete/{logId:Guid}")]
        public async Task<IActionResult> Delete(Guid logId, [FromForm] Log log)
        {
            if (logId != log.LogId) return NotFound();
            if (!ModelState.IsValid) return View(log);
            return await _logDb.LogDelete(log.LogId) == null
                ? (IActionResult)NotFound()
                : RedirectToAction(nameof(Index), "Batch", new { log.BatchId });
        }

    }
}