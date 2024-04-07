using System.ComponentModel.DataAnnotations;

namespace WebFormL1.ViewModel
{
    public class CommuneViewModel
    {
        public CommuneViewModel()
        {
        }
        public CommuneViewModel(int id, string name, string districtName, string? provinceName,string ?level)
        {
            Id = id;
            Name = name;
            DistrictName = districtName;
            ProvinceName = provinceName;
            Level = level;
        }
        public int Id { get; set; }
        [Display(Name = "Tên xã/phường")]
        public string? Name { get; set; }
        [Display(Name = "Quận/huyện")]
        public string? DistrictName { get; set; }
        [Display(Name = "Tỉnh/thành phố")]
        public string? ProvinceName { get; set; }
        [Display(Name = "Cấp bậc")]
        public string? Level { get; set; }
    }
}
