using ChristmasWishList.Shared.Requests;
using FluentValidation;

namespace ChristmasWishList.BusinessLayer.Validations
{
    public class SaveGiftRequestValidator : AbstractValidator<SaveGiftRequest>
    {
        public SaveGiftRequestValidator()
        {
            RuleFor(g => g.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("{PropertyName} is required");

            RuleFor(g => g.NameGift) 
                .NotNull()
                .NotEmpty()
                 .MaximumLength(250)
                .WithMessage("{PropertyName} is required");

            RuleFor(g => g.Price)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("{PropertyName} a valid price is required");

            RuleFor(g => g.IsPurchased)
                .NotNull()
                .WithMessage("{PropertyName} is required");

        }
    }
}
