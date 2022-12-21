using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Domain.AppCode.Extensions;
using Portfolio.Domain.Business.CategoryModule;
using Portfolio.Domain.Business.BlogModule;
using Portfolio.Domain.Business.UserModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IMediator mediator;

        public BlogController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(BlogAllQuery query)
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
        public async Task<IActionResult> Create(BlogCreateCommand command)
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

        public async Task<IActionResult> Edit(BlogSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            var users = await mediator.Send(new UserAllQuery());
            ViewBag.UserId = new SelectList(users, "Id", "Name");

            var command = new BlogEditCommand();//I will send BrandEditCommand to view
            command.Title = response.Title;
            command.Description = response.Description;
            command.ImagePath = response.ImagePath;
            command.UserId = response.UserId;
            return View(command);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BlogEditCommand command)
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

        public async Task<IActionResult> Details(BlogSingleQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Remove(BlogRemoveCommand command)
        {

            var response = await mediator.Send(command);


            if (response.Error)
            {
                return Json(response);
            }

            var data = await mediator.Send(new BlogAllQuery());

            return PartialView("_ListBody", data);
        }
    }
}
