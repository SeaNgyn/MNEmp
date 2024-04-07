using FluentValidation;
using WebFormL1.EditModel;

namespace WebFormL1.Validation
{
    public class CommuneErrorViewModel : AbstractValidator<CommuneEditModel>
    {
        public CommuneErrorViewModel()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Trường bắt buộc");
            RuleFor(x => x.DistrictId)
                .NotEmpty().WithMessage(x => "Trường bắt buộc");
            RuleFor(x => x.ProvinceId)
                .NotEmpty().WithMessage(x => "Trường bắt buộc");
            RuleFor(x => x.Level)
                .NotEmpty().WithMessage(x => "Trường bắt buộc");
        }
    }
}
