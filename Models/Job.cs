using System.ComponentModel.DataAnnotations;

namespace WebFormL1.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public string? JobName { get; set; }
    }
}
