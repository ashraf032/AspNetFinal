using MediatR;
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
    public class ContactMeCommand:IRequest<Contact>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public class ContactMeCommandHandler : IRequestHandler<ContactMeCommand, Contact>
        {
            private readonly PortfolioDbContext db;

            public ContactMeCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Contact> Handle(ContactMeCommand request, CancellationToken cancellationToken)
            {
                var data = new Contact
                {

                    Name = request.Name,
                    Email=request.Email,
                    Subject=request.Subject,
                    Message=request.Message
                };
                

                await db.Contacts.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
