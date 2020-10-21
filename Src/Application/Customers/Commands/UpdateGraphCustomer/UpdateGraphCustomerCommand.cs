﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Customers.Commands.UpdateGraphCustomer
{
    public class UpdateGraphCustomerCommand : IRequest<Customer>
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string Country { get; set; }

        public string Fax { get; set; }

        public string Phone { get; set; }

        public string PostalCode { get; set; }
    }

    public class UpdateGraphCustomerCommandHandler : IRequestHandler<UpdateGraphCustomerCommand, Customer>
    {
        private readonly INorthwindDbContext _context;

        public UpdateGraphCustomerCommandHandler(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Handle(UpdateGraphCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .SingleOrDefaultAsync(c => c.CustomerId == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            entity.Address = request.Address;
            entity.City = request.City;
            entity.CompanyName = request.CompanyName;
            entity.ContactName = request.ContactName;
            entity.ContactTitle = request.ContactTitle;
            entity.Country = request.Country;
            entity.Fax = request.Fax;
            entity.Phone = request.Phone;
            entity.PostalCode = request.PostalCode;

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}