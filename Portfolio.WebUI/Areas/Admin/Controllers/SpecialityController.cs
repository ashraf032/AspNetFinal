using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Business.SpecialtyModule;
using Portfolio.Domain.Business.UserModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialityController : Controller
    {
        private readonly IMediator mediator;

        public SpecialityController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(SpecialityAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpecialityCreateCommand command)
        {
            var response = await mediator.Send(command);

            if (response == null)
            {
                return View(command);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(SpecialitySingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SpecialityEditCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return View(command);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(SpecialitySingleQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Remove(SpecialityRemoveCommand command)
        {

            var response = await mediator.Send(command);


            if (response.Error)
            {
                return Json(response);
            }

            var data = await mediator.Send(new SpecialityAllQuery());

            return PartialView("_ListBody", data);
        }
    }
}
