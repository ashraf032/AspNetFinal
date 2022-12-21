using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class ContactSingleQuery : IRequest<Contact>
    {
        public int Id { get; set; }
        public class ContactSingleQueryHandler : IRequestHandler<ContactSingleQuery, Contact>
        {
            private readonly PortfolioDbContext db;

            public ContactSingleQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Contact> Handle(ContactSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Contacts.FirstOrDefaultAsync(s => s.Id == request.Id && s.DeletedDate==null, cancellationToken);
                return data;
            }
        }
    }
}
