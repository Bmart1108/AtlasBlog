using AtlasBlog.Data;
using AtlasBlog.Models;
using AtlasBlog.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics;
using X.PagedList;

namespace AtlasBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _appSettings;
        private readonly IEmailSender _emailSender;



        public HomeController(ILogger<HomeController> logger,
                                      ApplicationDbContext context,
                                      IEmailSender emailSender,
                                      IConfiguration appSettings)
        {
            _logger = logger;
            _context = context;
            _emailSender = emailSender;
            _appSettings = appSettings;
        }
        public async Task <IActionResult> ContactMe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactMe([Bind("Name,Email,Subject,Body")] ContactMe contactme)
        {
            //await _emailService.SendEmailAsync("Destination Email", "Subject", "Body")
            var emailSubject = $"Atlas Blog: {contactme.Subject}";
            var emailBody = $"{contactme.Body} <br/> {contactme.Name} <br/> {contactme.Email}";
            await _emailSender.SendEmailAsync(_appSettings["SmtpSettings:UserName"], emailSubject, emailBody);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index(int? pageNum)
        {
            //pagNum = pageNum ?? 1;
            pageNum ??= 1;

            //ToPagedList always needs to know what page to render
            //PagedList always need to be ordered explicitly
            var blogs = await _context.Blogs
                .OrderByDescending(b => b.Created)
                .ToPagedListAsync(pageNum, 4);

            return View(blogs);
        }

  


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}