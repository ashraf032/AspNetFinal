using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.AppCode.Services;
using Portfolio.Domain.Models.DataContext;
using Portfolio.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Domain.Business.ContactModule
{
    public class ContactEditCommand : IRequest<Contact>
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public class ContactEditCommandHandler : IRequestHandler<ContactEditCommand, Contact>
        {
            private readonly PortfolioDbContext db;
            private readonly EmailService emailService;

            public ContactEditCommandHandler(PortfolioDbContext db,EmailService emailService)
            {
                this.db = db;
                this.emailService = emailService;
            }
            public async Task<Contact> Handle(ContactEditCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Contacts.FirstOrDefaultAsync(s => s.Id == request.Id && s.DeletedDate==null, cancellationToken);
                if (data == null)
                {
                    return null;
                }
                data.Answer = request.Answer;
                data.AnswerDate = DateTime.UtcNow.AddHours(4);
                await emailService.SendEmailAsync(data.Email, data.Answer);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
