using System.ComponentModel.DataAnnotations;

namespace WebFormL1.ViewModel
{
    public class DistrictViewModel
    {
        public DistrictViewModel()
        {
        }
        public DistrictViewModel(int id, string? name, string? provinceName,string? level)
        {
            Id = id;
            Name = name;
            ProvinceName = provinceName;
            Level = level;
        }
        public int Id { get; set; }
        [Display(Name = "Tên quận/huyện")]
        public string? Name { get; set; }
        [Display(Name = "Tỉnh/thành phố")]
        public string? ProvinceName { get; set; }
        [Display(Name = "Cấp bậc")]
        public string? Level { get; set; }
    }
}

