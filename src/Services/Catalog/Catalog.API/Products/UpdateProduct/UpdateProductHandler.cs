﻿
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, string ImageFile, List<string> Category)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler.Handle called {@Command}", command);
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundException();
            }
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageFile = command.ImageFile;
            product.Category = command.Category;
            session.Update(product);
            await session.SaveChangesAsync();
            return new UpdateProductResult(true);
        }
    }
}
