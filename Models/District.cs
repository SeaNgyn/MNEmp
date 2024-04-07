using System.ComponentModel.DataAnnotations;
using WebFormL1.BusinessLogic;

namespace WebFormL1.Models
{
    public class District
    {
        public District() { }
        public District(int id, string? name,string? level, int provinceId)
        {
            Id = id;
            Name = name;
            Level = level;
            ProvinceId = provinceId;
        }
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }
        public int ProvinceId { get; set; }
        public Province? Province { get; set; }
        public ICollection<Commune>? Communes { get; set; }
    }
}
