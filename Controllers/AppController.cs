using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RebeccaResources.Data;
using RebeccaResources.Services;
using RebeccaResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebeccaResources.Controllers
{
    public class AppController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IBelleBakeryRepository repository;

        public AppController(IEmailService emailService, IBelleBakeryRepository repository)
        {
            _emailService = emailService;
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost("Contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // send the email
                _emailService.SendEmail("crispyporter86@gmail.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            else
            {
                // show errors
            }
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }


        public IActionResult Shop()
        {

            return View();
        }
    }
}
