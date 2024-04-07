using System.ComponentModel.DataAnnotations;

namespace WebFormL1.Models
{
    public class Ethnic
    {
        [Key]
        public int Id { get; set; }
        public string? EthnicName { get; set; }
    }
}
