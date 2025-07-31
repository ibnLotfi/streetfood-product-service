using FluentValidation;
using Product.Domain.Enums;

namespace Product.Application.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required")
            .Must(BeAValidCategory).WithMessage("Invalid category");

        RuleFor(x => x.BasePrice)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        When(x => x.IsCustomizable, () =>
        {
            RuleFor(x => x.AvailableSizes)
                .NotEmpty().WithMessage("Customizable products must have available sizes");
        });
    }

    private bool BeAValidCategory(string category)
    {
        return Enum.TryParse<ProductCategory>(category, true, out _);
    }
}