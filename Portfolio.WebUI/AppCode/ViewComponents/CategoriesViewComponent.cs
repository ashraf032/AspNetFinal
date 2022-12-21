using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Business.CategoryModule;
using System.Threading.Tasks;

namespace Portfolio.WebUI.AppCode.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly IMediator mediator;

        public CategoriesViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await mediator.Send(new CategoryAllQuery());
            return View(response);
        }
    }
}
