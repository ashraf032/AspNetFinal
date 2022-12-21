using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Business.AccountModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [AllowAnonymous]
        [Route("/signin.html")]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("/signin.html")]

        public async Task<IActionResult> Signin(SigninCommand command)
        {
            var result = await mediator.Send(command);
            if (result == true)
            {
                var redirectUrl = Request.Query["ReturnUrl"];
                if (string.IsNullOrWhiteSpace(redirectUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(redirectUrl);
            }
            return View(command);
        }
    }
}
