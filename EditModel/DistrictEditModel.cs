using System.ComponentModel.DataAnnotations;
using WebFormL1.Models;

namespace WebFormL1.EditModel
{
    public class DistrictEditModel
    {
        public DistrictEditModel()
        {
        }
        public DistrictEditModel(int id, string? name, string? level, int provinceId)
        {
            Id = id;
            Name = name;
            Level = level;
            ProvinceId = provinceId;
        }
        public int Id { get; set; }
        [Display(Name = "Tên quận/huyện")]
        public string? Name { get; set; }
        [Display(Name = "Cấp Bậc")]
        public string? Level { get; set; }
        [Display(Name = "Tỉnh/thành phố")]
        public int ProvinceId { get; set; }
        public List<Province>? ProvinceList { get; set; }
    }
}
