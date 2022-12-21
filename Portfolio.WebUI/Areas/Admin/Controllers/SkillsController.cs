using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Domain.Business.SkillModule;
using Portfolio.Domain.Business.SpecialtyModule;
using Portfolio.Domain.Business.UserModule;
using Portfolio.Domain.Models.Entities;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillsController : Controller
    {
        private readonly IMediator mediator;

        public SkillsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(SkillAllQuery query)
        {
            var response = await mediator.Send(query);
            if (response==null)
            {
                return null;
            }
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            var user = await mediator.Send(new UserAllQuery());
            ViewBag.UserId = new SelectList(user, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SkillCreateCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return View(command);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(SkillSingleQuery query)
        {
            var user = await mediator.Send(new UserAllQuery());
            ViewBag.UserId = new SelectList(user, "Id", "Name");
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SkillEditCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return View(command);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(SkillSingleQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Remove(SkillRemoveCommand command)
        {

            var response = await mediator.Send(command);


            if (response.Error)
            {
                return Json(response);
            }

            var data = await mediator.Send(new SkillAllQuery());

            return PartialView("_ListBody", data);
        }
    }
}
