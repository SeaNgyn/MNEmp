using System.ComponentModel.DataAnnotations;
using WebFormL1.Models;

namespace WebFormL1.EditModel
{
    public class CommuneEditModel
    {
        public CommuneEditModel()
        {
        }
        public CommuneEditModel(int id, string name,string? level, int districtId, int provinceId)
        {
            Id = id;
            Name = name;
            Level = level;
            DistrictId = districtId;
            ProvinceId = provinceId;
        }
        public int Id { get; set; }
        [Display(Name = "Tên xã/phường")]
        public string? Name { get; set; }
        [Display(Name = "Quận/huyện")]
        public int DistrictId { get; set; }
        [Display(Name = "Tỉnh/thành phố")]
        public int ProvinceId { get; set; }
        [Display(Name = "Cấp Bậc")]
        public string? Level { get; set; }
        public List<District>? DistrictsByProvince { get; set; }
    }
}
