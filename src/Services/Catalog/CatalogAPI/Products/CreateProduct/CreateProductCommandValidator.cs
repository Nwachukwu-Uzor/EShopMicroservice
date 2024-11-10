namespace CatalogAPI.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} is required}")
            .MinimumLength(2).WithMessage("{PropertyName} must be at least {MinLength} characters");

        RuleFor(p => p.Description).NotEmpty().WithMessage("{PropertyName} is required}")
            .MinimumLength(2).WithMessage("{PropertyName} must be at least {MinLength} characters"); 
        RuleFor(p => p.ImageFile).NotEmpty().WithMessage("{PropertyName} is required}")
            .MinimumLength(2).WithMessage("{PropertyName} must be at least {MinLength} characters")
            .Must(IsValidUrl).WithMessage("{PropertyName} must be a valid URL");

        RuleFor(p => p.Price).NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        
        RuleFor(p => p.Category).NotEmpty().WithMessage("{PropertyName} is required")
            .ForEach(c => c.NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(2).WithMessage("{PropertyName} must be at least {MinLength} characters}")
                .Must(str => !string.IsNullOrWhiteSpace(str)).WithMessage("{PropertyName} must not be empty or whitespace."));
    }

    private static bool IsValidUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        // Try to create a Uri from the string
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}