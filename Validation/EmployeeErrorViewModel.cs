using FluentValidation;
using WebFormL1.BusinessLogic;
using WebFormL1.Constant;
using WebFormL1.EditModel;
using WebFormL1.Interface;
using WebFormL1.Models;

namespace WebFormL1.Validation
{
    public class EmployeeErrorViewModel : AbstractValidator<EmployeeEditModel>
    {
        private readonly IEmployeeBL _bl;
        public EmployeeErrorViewModel(IEmployeeBL bl)
        {
            _bl = bl;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Trường bắt buộc")
                .Matches(ConstantName.NamePattern).WithMessage("Tên không được chỉ có số và để trống");
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Trường bắt buộc")
                .GreaterThanOrEqualTo(new DateTime(ConstantName.MinYearOfBirthDate, ConstantName.MinMonthOfBirthDate, ConstantName.MinDayOfBirthDate)).WithMessage($"Ngày phải sau {ConstantName.MinDayOfBirthDate}/{ConstantName.MinMonthOfBirthDate}/{ConstantName.MinYearOfBirthDate}")
                .LessThan(DateTime.Now).WithMessage($"Ngày phải trước phải hiện tại");
            RuleFor(x => x.EthnicId)
                .NotEmpty().WithMessage("Trường bắt buộc");
            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("Trường bắt buộc");
            RuleFor(x => x.IdentifyCardNumber)
                .Matches(ConstantName.IndentityPattern).WithMessage("Chỉ chứa các chữ số viết liền.CCCD cần có 12 chữ số")
                .Must((model, cardId) => BeUniqueIdentifyCardNumber(cardId, model.Id)).WithMessage("Không trùng lặp số CCCD");
            RuleFor(x => x.PhoneNumber)
                .Matches(ConstantName.PhonePattern).WithMessage("Viết thao dạng +XXX (XX) XXXX-XXXX or XXX-XXXX-XXXX or XXX.XXXX.XXXX")
                .Must((model, phone) => BeUniquePhoneNumber(phone, model.Id)).WithMessage("Không trùng lặp số điện thoại");
            RuleFor(x => x.ProvinceId)
                .NotEmpty().WithMessage("Trường bắt buộc");
            RuleFor(x => x.DistrictId)
                .NotEmpty().WithMessage("Trường bắt buộc");
            RuleFor(x => x.CommuneId)
                .NotEmpty().WithMessage("Trường bắt buộc");
        }
        private bool BeUniquePhoneNumber(string phoneNumber, int currentEmployeeId)
        {
            return _bl.IsUniquePhoneNumber(phoneNumber, currentEmployeeId);
        }
        private bool BeUniqueIdentifyCardNumber(string cardId, int currentEmployeeId)
        {
            return _bl.IsUniqueIdentifyCardNumber(cardId, currentEmployeeId);
        }
    }
}
