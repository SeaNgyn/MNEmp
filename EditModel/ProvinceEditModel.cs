using System.ComponentModel.DataAnnotations;

namespace WebFormL1.EditModel
{
    public class ProvinceEditModel
    {
        public ProvinceEditModel()
        {
        }
        public ProvinceEditModel(int id, string? name,string? level)
        {
            Id = id;
            Name = name;
            Level= level;
        }
        public int Id { get; set; }
        [Display(Name = "Tên tỉnh/thành phố")]
        public string? Name { get; set; }
        [Display(Name = "Cấp Bậc")]
        public string? Level { get; set; }
    }
}
