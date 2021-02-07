using System.Threading;
using System.Threading.Tasks;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Mappings;
using CarsManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CarsManager.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<int>, IMapFrom<CreateEmployeeDto>
    {
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public int TownId { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly IImageManager imageManager;

        public CreateEmployeeCommandHandler(IApplicationDbContext context, IImageManager imageManager)
        {
            this.context = context;
            this.imageManager = imageManager;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var town = await context.Towns.FindAsync(request.TownId);
            if (town == null)
                throw new NotFoundException(nameof(Town), request.TownId);

            var entity = new Employee
            {
                GivenName = request.GivenName,
                MiddleName = request.MiddleName,
                Surname = request.Surname,
                Town = town,
                Address = request.Address,
                PostCode = request.PostCode,
                Telephone = request.Telephone,
            };

            entity.ImageName = request.Photo == null
                ? string.Empty
                : await imageManager.SaveFileAsync(request.PhotoPath, request.Photo, cancellationToken);

            await context.Employees.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
