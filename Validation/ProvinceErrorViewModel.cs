using FluentValidation;
using WebFormL1.EditModel;

namespace WebFormL1.Validation
{
    public class ProvinceErrorViewModel : AbstractValidator<ProvinceEditModel>
    {
        public ProvinceErrorViewModel()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Trường bắt buộc");
            RuleFor(x => x.Level)
                .NotEmpty().WithMessage("Trường bắt buộc");
        }
    }
}
