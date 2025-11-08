using FluentValidation;
using Portal.Api.DTOs;

namespace Portal.Api.Validators
{
    public class UcHarfKelimeValidator : AbstractValidator<UcHarfKelimeDto>
    {
        public UcHarfKelimeValidator()
        {
            RuleFor(x => x.Tanim)
                .NotEmpty().WithMessage("Tanım alanı boş olamaz.")
                .MinimumLength(3).WithMessage("Tanım en az 3 karakter olmalı.")
                .MaximumLength(3).WithMessage("Tanım en fazla 3 karakter olabilir.");

            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("ID negatif olamaz.");
        }
    }
}
