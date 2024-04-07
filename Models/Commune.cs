using System.ComponentModel.DataAnnotations;
using WebFormL1.BusinessLogic;

namespace WebFormL1.Models
{
    public class Commune
    {
        public Commune()
        {
        }
        public Commune(int id, string name,string level,int districtId)
        {
            Id = id;
            Name = name;
            Level =level;
            DistrictId = districtId;
        }
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }
        public int DistrictId { get; set; }
        public District? District { get; set; }
    }
}
