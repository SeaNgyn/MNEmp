using WebFormL1.Models;

namespace WebFormL1.SeedData
{
    public class SeedJob
    {
        public List<Job> Jobs { get; set; }
        public SeedJob()
        {
            Jobs = new List<Job>()
            {
               new()  { Id = 1, JobName = "Software Developer" },
               new () { Id = 2, JobName = "Marketing Manager" },
               new() { Id = 3, JobName = "Carpenter" },
               new() { Id = 4, JobName = "Laborer" },
               new() { Id = 5, JobName = "Bricklayer" },
               new() { Id = 6, JobName = "Mason" },
               new() { Id = 7, JobName = "Welder" },
               new() { Id = 8, JobName = "Miner" },
               new() { Id = 9, JobName = "Printer" },
               new() { Id = 10, JobName = "Plater" },
            };
        }
    }
}
