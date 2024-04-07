namespace WebFormL1.Models
{
    public class Province
    {
        public Province()
        {
        }
        public Province(int id, string? name,string? level)
        {
            Id = id;
            Name = name;
            Level = level;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }
        public ICollection<District>? Districts { get; set; }
    }
}

