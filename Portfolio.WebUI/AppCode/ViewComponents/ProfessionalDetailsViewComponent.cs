using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Business.UserModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.AppCode.ViewComponents
{
    public class ProfessionalDetailsViewComponent:ViewComponent
    {
        private readonly IMediator mediator;

        public ProfessionalDetailsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = new UserMeQuery();
            var response = await mediator.Send(query);
            return View(response);
        }
    }
}
