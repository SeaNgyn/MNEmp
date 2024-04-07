using System.ComponentModel.DataAnnotations;
using WebFormL1.BusinessLogic;

namespace WebFormL1.Models
{
    public class Degree
    {
        public Degree(int iD, string name, DateTime? dateOfIssue,
                    int provinceId, DateTime? expirationDate, int employeeId)
        {
            ID = iD;
            Name = name;
            DateOfIssue = dateOfIssue;
            ProvinceId = provinceId;
            ExpirationDate = expirationDate;
            EmployeeId = employeeId;
        }
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfIssue { get; set; }
        public int ProvinceId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Province? Province { get; set; }
    }
}
