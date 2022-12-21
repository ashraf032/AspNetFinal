using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Portfolio.Domain.AppCode.Extensions;
using Portfolio.Domain.Business.ExperienceModule;
using Portfolio.Domain.Business.ProjectModule;
using Portfolio.Domain.Business.ServiceModule;
using Portfolio.Domain.Business.UserModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly IMediator mediator;
        private readonly IHostEnvironment env;

        public ExperienceController(IMediator mediator,IHostEnvironment env)
        {
            this.mediator = mediator;
            this.env = env;
        }
        public async Task<IActionResult> Index(ExperienceAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            var users = await mediator.Send(new UserAllQuery());
            ViewBag.UserId = new SelectList(users, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ExperienceCreateCommand command)
        {
            
                if (command.Image.CheckFileType("image/") == false)
                {
                    ModelState.AddModelError("Image", "Zehmet olmasa sekil gonderin");
                }
            
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            var users = await mediator.Send(new UserAllQuery());
            ViewBag.UserId = new SelectList(users, "Id", "Name");

            return View(command);

        }

        public async Task<IActionResult> Edit(ExperienceSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            var users = await mediator.Send(new UserAllQuery());
            ViewBag.UserId = new SelectList(users, "Id", "Name");

            var categories = await mediator.Send(new ServiceAllQuery());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            var command = new ExperienceEditCommand();//I will send ExperienceEditCommand to view
            command.Name = response.Name;
            command.CompanyName = response.CompanyName;
            command.CompanyLocation = response.CompanyLocation;
            command.Description = response.Description;
            command.CompanyImagePath = response.CompanyImagePath;
            command.UserId = response.UserId;
            command.ExperienceType = response.ExperienceType;
            return View(command);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ExperienceEditCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                var users = await mediator.Send(new UserAllQuery());
                ViewBag.UserId = new SelectList(users, "Id", "Name");

                return View(command);
            }


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(ExperienceSingleQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Remove(ExperienceRemoveCommand command)
        {

            var response = await mediator.Send(command);


            if (response.Error)
            {
                return Json(response);
            }

            var data = await mediator.Send(new ExperienceAllQuery());

            return PartialView("_ListBody", data);
        }
    }
}
