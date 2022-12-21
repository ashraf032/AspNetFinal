using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Domain.Business.AbilityModule;
using Portfolio.Domain.Business.UserModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AbilityController : Controller
    {
        private readonly IMediator mediator;

        public AbilityController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(AbilityAllQuery query)
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
        public async Task<IActionResult> Create(AbilityCreateCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return View(command);
            }
            var users = await mediator.Send(new UserAllQuery());
            ViewBag.UserId = new SelectList(users, "Id", "Name");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(AbilitySingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            var users = await mediator.Send(new UserAllQuery());
            ViewBag.UserId = new SelectList(users, "Id", "Name");
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AbilityEditCommand command)
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

        public async Task<IActionResult> Details(AbilitySingleQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Remove(AbilityRemoveCommand command)
        {

            var response = await mediator.Send(command);


            if (response.Error)
            {
                return Json(response);
            }

            var data = await mediator.Send(new AbilityAllQuery());

            return PartialView("_ListBody", data);
        }
    }
}
