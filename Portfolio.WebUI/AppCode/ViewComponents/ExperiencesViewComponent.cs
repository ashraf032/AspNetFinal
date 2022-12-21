using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Business.ExperienceModule;
using Portfolio.Domain.Business.SkillModule;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.WebUI.AppCode.ViewComponents
{
    public class ExperiencesViewComponent:ViewComponent
    {
        private readonly IMediator mediator;

        public ExperiencesViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await mediator.Send(new ExperienceAllQuery());
            return View(response);
        }
    }
}
