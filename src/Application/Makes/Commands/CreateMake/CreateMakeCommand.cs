﻿using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using MediatR;

namespace CarsManager.Application.Makes.Commands.CreateMake
{
    public class CreateMakeCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateMakeCommandHandler : IRequestHandler<CreateMakeCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateMakeCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateMakeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Make
            {
                Name = request.Name
            };

            await context.Makes.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}