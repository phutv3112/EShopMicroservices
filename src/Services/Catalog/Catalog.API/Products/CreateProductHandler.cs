using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create Product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                ImageFile = command.ImageFile,
                Category = command.Category
            };
            // Save Product entity to database

            // Return CreateProductResult object with Id of the newly created Product
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
