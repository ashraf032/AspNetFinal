using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Domain.Business.ServiceModule;
using Portfolio.Domain.Business.SkillModule;
using Portfolio.Domain.Business.SpecialtyModule;
using Portfolio.Domain.Business.UserModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IMediator mediator;

        public ServiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(ServiceAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            var specialities = await mediator.Send(new SpecialityAllQuery());
            ViewBag.SpecialityId = new SelectList(specialities, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return View(command);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(ServiceSingleQuery query)
        {
            var specialities = await mediator.Send(new SpecialityAllQuery());
            ViewBag.SpecialityId = new SelectList(specialities, "Id", "Name");
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ServiceEditCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return View(command);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(ServiceSingleQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Remove(ServiceRemoveCommand command)
        {

            var response = await mediator.Send(command);


            if (response.Error)
            {
                return Json(response);
            }

            var data = await mediator.Send(new ServiceAllQuery());

            return PartialView("_ListBody", data);
        }
    }
}
