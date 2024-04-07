using System.ComponentModel.DataAnnotations;
using WebFormL1.Models;

namespace WebFormL1.EditModel
{
    public class EmployeeEditModel
    {
        public EmployeeEditModel()
        {
        }
        public EmployeeEditModel(int id, string? name, DateTime dateOfBirth, int ethnicID,
                                    int jobID, string? identifyCardNumber, string? phoneNumber, int? provinceID,
                                    int? districtID, int? communeID)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            EthnicId = ethnicID;
            JobId = jobID;
            IdentifyCardNumber = identifyCardNumber;
            PhoneNumber = phoneNumber;
            ProvinceId = provinceID;
            DistrictId = districtID;
            CommuneId = communeID;
        }
        public int Id { get; set; }
        [Display(Name = "Họ tên")]
        public string? Name { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Dân tộc")]
        public int EthnicId { get; set; }
        [Display(Name = "Nghề nghiệp")]
        public int JobId { get; set; }
        [Display(Name = "Căn cước công dân")]
        public string? IdentifyCardNumber { get; set; }
        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Tỉnh/Thành phố")]
        public int? ProvinceId { get; set; }
        [Display(Name = "Quận/Huyện")]
        public int? DistrictId { get; set; }
        [Display(Name = "Xã/Phường")]
        public int? CommuneId { get; set; }
        public List<District>? Districts { get; set; }
        public List<Commune>? Communes { get; set; }
    }
}
