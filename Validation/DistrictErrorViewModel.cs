using FluentValidation;
using WebFormL1.EditModel;

namespace WebFormL1.Validation
{
    public class DistrictErrorViewModel : AbstractValidator<DistrictEditModel>
    {
        public DistrictErrorViewModel()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Trường bắt buộc");
            RuleFor(x => x.ProvinceId)
                .NotEmpty().WithMessage("Trường bắt buộc");
            RuleFor(x => x.Level)
                .NotEmpty().WithMessage("Trường bắt buộc");
        }
    }
}
