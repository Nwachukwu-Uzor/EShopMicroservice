using BuildingBlocks.Extensions;

namespace CatalogAPI.Products.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("{PropertyName} is required.}")
            .Must(id => id.ToString().IsValidGuid()).WithMessage("{PropertyName} is not a valid id");
    }
}