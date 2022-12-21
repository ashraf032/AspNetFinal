using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portfolio.Domain.Business.ContactModule;
using Portfolio.Domain.Business.SpecialtyModule;
using Portfolio.Domain.Business.UserModule;
using Portfolio.Domain.Models.DataContext;
using Portfolio.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IMediator mediator;
        private readonly PortfolioDbContext dbContext;

        public HomeController(IMediator mediator,PortfolioDbContext dbContext)
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index(UserMeQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        public async Task<IActionResult> Resume(UserMeQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(ContactMeCommand command)
        {
            var response = await mediator.Send(command);
            
            TempData["message"] = "Thank You. Your Message has been Submitted";
            return RedirectToAction(nameof(Contact));
        }

    }
}
