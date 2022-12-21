using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Domain.AppCode.Extensions;
using Portfolio.Domain.Business.CategoryModule;
using Portfolio.Domain.Business.ProjectModule;
using Portfolio.Domain.Business.ServiceModule;
using Portfolio.Domain.Business.SpecialtyModule;
using Portfolio.Domain.Business.UserModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly IMediator mediator;

        public ProjectController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(ProjectAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            var users = await mediator.Send(new UserAllQuery());
            ViewBag.UserId = new SelectList(users, "Id", "Name");

            var categories = await mediator.Send(new CategoryAllQuery());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateCommand command)
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

            var categories = await mediator.Send(new CategoryAllQuery());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            return View(command);

        }

        public async Task<IActionResult> Edit(ProjectSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            var users = await mediator.Send(new UserAllQuery());
            ViewBag.UserId = new SelectList(users, "Id", "Name");

            var categories = await mediator.Send(new CategoryAllQuery());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            var command = new ProjectEditCommand();//I will send BrandEditCommand to view
            command.Name = response.Name;
            command.OnlinePath = response.OnlinePath;
            command.ImagePath = response.ImagePath;
            command.UserId = response.UserId;
            command.CategoryId = response.CategoryId;
            return View(command);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectEditCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                var users = await mediator.Send(new UserAllQuery());
                ViewBag.UserId = new SelectList(users, "Id", "Name");

                var categories = await mediator.Send(new CategoryAllQuery());
                ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

                return View(command);
            }


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(ProjectSingleQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Remove(ProjectRemoveCommand command)
        {

            var response = await mediator.Send(command);


            if (response.Error)
            {
                return Json(response);
            }

            var data = await mediator.Send(new ProjectAllQuery());

            return PartialView("_ListBody", data);
        }
    }
}
