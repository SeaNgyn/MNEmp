using System.ComponentModel.DataAnnotations;

namespace WebFormL1.Models
{
    public class Employee
    {
        public Employee(int id, string? name, DateTime dateOfBirth,  int ethnicId,
                                    int jobId, string? identityCardNumber, string? phoneNumber, int? provinceId,
                                    int? districtId, int? communeId)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            EthnicId = ethnicId;
            JobId = jobId;
            IdentityCardNumber = identityCardNumber;
            PhoneNumber = phoneNumber;
            ProvinceId = provinceId;
            DistrictId = districtId;
            CommuneId = communeId;
        }
        public Employee()
        { 
        }  
        public int Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public int JobId { get; set; }
        public int EthnicId { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? CommuneId { get; set; }
        public int? DepartmentId { get; set; }
        public virtual District? District { get; set; }
        public virtual Job? Job { get; set; }
        public virtual Ethnic? Ethnic { get; set; }
        public virtual Commune? Commune { get; set; }
        public virtual Province? Province { get; set; }
        public virtual List<Degree>? Degrees { get; set; }
        public virtual Department? Department { get; set; }
    }
}
