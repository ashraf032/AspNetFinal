using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Business.UserModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(UserAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        public async Task<IActionResult> Edit(UserSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response==null)
            {
                return NotFound();
            }
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return View(command);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(UserSingleQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }
    }
}
