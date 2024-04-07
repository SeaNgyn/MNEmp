using System.ComponentModel.DataAnnotations;

namespace WebFormL1.ViewModel
{
    public class ProvinceViewModel
    {
        public ProvinceViewModel()
        {
        }
        public ProvinceViewModel(int id, string? name,string? level)
        {
            Id = id;
            Name = name;
            Level = level;
        }
        public int Id { get; set; }
        [Display(Name = "Tên tỉnh/thành phố")]
        public string? Name { get; set; }
        [Display(Name = "Cấp bậc")]
        public string? Level { get; set; }
    }
}
