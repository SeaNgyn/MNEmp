using System.ComponentModel.DataAnnotations;

namespace WebFormL1.ViewModel
{
    public class DegreeViewModel
    {
        public DegreeViewModel(int id, string name, DateTime? dateOfIssue, string provinceName,
                         DateTime? expirationDate, string employeeName, int employeeId)
        {
            Id = id;
            Name = name;
            DateOfIssue = dateOfIssue;
            ProvinceName = provinceName;
            ExpirationDate = expirationDate;
            EmployeeName = employeeName;
            EmployeeId = employeeId;
        }
        public int Id { get; set; }
        [Display(Name = "Tên văn bằng")]
        public string? Name { get; set; }
        [Display(Name = "Ngày cấp")]
        [DataType(DataType.Date)]
        public DateTime? DateOfIssue { get; set; }
        [Display(Name = "Đơn vị cấp")]
        public string? ProvinceName { get; set; }
        [Display(Name = "Ngày hết hạn")]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
        [Display(Name = "Nhân viên")]
        public string? EmployeeName { get; set; }
        public int EmployeeId { get; set; }
    }
}

