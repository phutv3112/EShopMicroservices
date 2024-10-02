namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
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
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // Return CreateProductResult object with Id of the newly created Product
            return new CreateProductResult(product.Id);
        }
    }
}
