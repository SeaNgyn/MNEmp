using System.ComponentModel.DataAnnotations;

namespace WebFormL1.EditModel
{
    public class DegreeEditModel
    {
        public DegreeEditModel()
        {
        }
        public DegreeEditModel(int id, string name, DateTime? dateOfIssue,
                    int provinceId, DateTime? expirationDate, int employeeId)
        {
            Id = id;
            Name = name;
            DateOfIssue = dateOfIssue;
            ProvinceId = provinceId;
            ExpirationDate = expirationDate;
            EmployeeId = employeeId;
        }
        public int Id { get; set; }
        [Display(Name = "Tên văn bằng")]
        public string? Name { get; set; }
        [Display(Name = "Ngày cấp")]
        [DataType(DataType.Date)]
        public DateTime? DateOfIssue { get; set; }
        [Display(Name = "Đơn vị cấp")]
        public int ProvinceId { get; set; }
        [Display(Name = "Ngày hết hạn")]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
        [Display(Name = "Nhân viên")]
        public int EmployeeId { get; set; }
    }
}
