using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Business.CategoryModule;
using Portfolio.Domain.Business.ProjectModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.AppCode.ViewComponents
{
    public class ProjectItemsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public ProjectItemsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await mediator.Send(new ProjectAllQuery());
            return View(response);
        }
    }
}
