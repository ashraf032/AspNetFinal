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
    public class ContactAllQuery:IRequest<List<Contact>>
    {
        public class ContactAllQueryHandler : IRequestHandler<ContactAllQuery, List<Contact>>
        {
            private readonly PortfolioDbContext db;

            public ContactAllQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Contact>> Handle(ContactAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Contacts.Where(c=>c.AnswerDate==null && c.DeletedDate==null)
                    .ToListAsync(cancellationToken);
                if (data == null)
                {
                    return null;
                }
                return data;
            }
        }
    }
}
