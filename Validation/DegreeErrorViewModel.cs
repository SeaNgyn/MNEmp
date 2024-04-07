using FluentValidation;
using WebFormL1.Constant;
using WebFormL1.EditModel;

namespace WebFormL1.Validation
{
    public class DegreeErrorViewModel : AbstractValidator<DegreeEditModel>
    {
        public DegreeErrorViewModel()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Trường bắt buộc");

            RuleFor(x => x.DateOfIssue)
                .NotEmpty().WithMessage("Trường bắt buộc")
                .GreaterThanOrEqualTo(new DateTime(ConstantName.MinYearOfBirthDate, ConstantName.MinMonthOfBirthDate, ConstantName.MinDayOfBirthDate)).WithMessage($"Ngày phải sau {ConstantName.MinDayOfBirthDate}/{ConstantName.MinMonthOfBirthDate}/{ConstantName.MinYearOfBirthDate}")
                .LessThan(DateTime.Now).WithMessage($"Ngày phải trước ngày hiện tại");

            RuleFor(x => x.ProvinceId)
                .NotEmpty().WithMessage(x => "Trường bắt buộc");

            RuleFor(x => x.ExpirationDate)
                .NotEmpty().WithMessage("Trường bắt buộc")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage($"Ngày phải sau ngày hiện tại")
                .LessThan(new DateTime(ConstantName.MaxYear,ConstantName.MaxMonth,ConstantName.MaxDay)).WithMessage($"Ngày phải sau {ConstantName.MaxDay}/{ConstantName.MaxMonth}/{ConstantName.MaxYear}");
        }
    }
}
