using System.ComponentModel.DataAnnotations;
using WebFormL1.Models;

namespace WebFormL1.ViewModel
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            GetAddress();
        }
        public EmployeeViewModel(int id, string? name, DateTime dateOfBirth, string ethnicityName,
                                    string jobName, string? identityCardNumber, string? phoneNumber, string? provinceName,
                                    string? districtName, string? communeName,  int numberOfDegrees)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = CalculateAge();
            EthnicityName = ethnicityName;
            JobName = jobName;
            IdentityCardNumber = identityCardNumber;
            PhoneNumber = phoneNumber;
            ProvinceName = provinceName;
            DistrictName = districtName;
            CommuneName = communeName;
            NumberOfDegrees = numberOfDegrees;
            GetAddress();
        }
        public EmployeeViewModel(int id, string? name, DateTime dateOfBirth, string ethnicityName,
                                    string jobName, string? identityCardNumber, string? phoneNumber, string? provinceName,
                                    string? districtName, string? communeName, int numberOfDegrees, int attendedSessions,int sessions)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = CalculateAge();
            EthnicityName = ethnicityName;
            JobName = jobName;
            IdentityCardNumber = identityCardNumber;
            PhoneNumber = phoneNumber;
            ProvinceName = provinceName;
            DistrictName = districtName;
            CommuneName = communeName;
            NumberOfDegrees = numberOfDegrees;
            GetAddress();
            AttendedSessions = attendedSessions;
            Sessions = sessions;
        }
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Họ tên")]
        public string? Name { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Tuổi")]
        public int Age { get; set; }
        [Display(Name = "Dân tộc")]
        public string? EthnicityName { get; set; }
        [Display(Name = "Nghề nghiệp")]
        public string? JobName { get; set; }
        [Display(Name = "Căn cước công dân")]
        public string? IdentityCardNumber { get; set; }
        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Tỉnh/Thành phố")]
        public string? ProvinceName { get; set; }
        [Display(Name = "Quận/Huyện")]
        public string? DistrictName { get; set; }
        [Display(Name = "Xã/Phường")]
        public string? CommuneName { get; set; }
        [Display(Name = "Số văn bằng")]
        public int NumberOfDegrees { get; set; }
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }
        public void GetAddress()
        {
                Address = $"{CommuneName}, {DistrictName}, {ProvinceName}";
        }
        public int CalculateAge()
        {
            DateTime currentDate = DateTime.Now;
            DateTime dob = DateOfBirth;
            int age = currentDate.Year - dob.Year;
            return age;
        }
        public int AttendedSessions { get; set; }
        public int Sessions { get; set; }
    }
}

