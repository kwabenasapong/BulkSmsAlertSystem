using BulkSmsAlertSystem.Data;
using BulkSmsAlertSystem.Models;
using BulkSmsAlertSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkSmsAlertSystem.Controllers
{
    public class SmsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SmsService _smsService;

        public SmsController(ApplicationDbContext context, SmsService smsService)
        {
            _context = context;
            _smsService = smsService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.SmsMessages.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipientNumber,Message")] SmsMessage smsMessage)
        {
            if (ModelState.IsValid)
            {
                await _smsService.SendSmsAsync(smsMessage.RecipientNumber, smsMessage.Message);
                smsMessage.Message += TempData["SignOff"];
                _context.Add(smsMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(smsMessage);
        }

        public async Task<IActionResult> History(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smsMessage = await _context.SmsMessages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (smsMessage == null)
            {
                return NotFound();
            }

            return View(smsMessage);
        }
    }
}
