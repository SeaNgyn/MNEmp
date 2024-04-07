using WebFormL1.Models;

namespace WebFormL1.SeedData
{
    public class SeedEmployee
    {

        public List<Employee> Employees { get; set; }
        public SeedEmployee()
        {
            Employees = new List<Employee>()
            {
            new Employee(1, "John Doe", new DateTime(1990, 5, 15), 1, 3, "123456789", "555-1234", 1, 1, 1),
            new Employee(2, "Jane Doe", new DateTime(1985, 8, 22), 2, 3, "987654321", "555-5678", 1, 1, 1),
            new Employee(3, "Alice Smith", new DateTime(1988, 3, 10), 3, 3, "456789012", "555-9876", 1, 1, 1),
            new Employee(4, "Bob Johnson", new DateTime(1995, 7, 5), 4, 3, "789012345", "555-4321", 1, 1, 1),
            new Employee(5, "Charlie Brown", new DateTime(1983, 11, 18), 5, 3, "345678901", "555-8765", 1, 1, 1),
            new Employee(6, "Diana Williams", new DateTime(1992, 9, 25), 6, 3, "234567890", "555-2345", 1, 1, 1),
            new Employee(7, "Edward Davis", new DateTime(1987, 6, 8), 1, 3, "567890123", "555-6789", 1, 1, 1),
            new Employee(8, "Fiona Miller", new DateTime(1994, 1, 30), 8, 3, "901234567", "555-3456", 1, 1, 1),
            new Employee(9, "George Wilson", new DateTime(1981, 12, 12), 9, 3, "012345678", "555-7890", 1, 1, 1),
            new Employee(10, "Helen Anderson", new DateTime(1989, 4, 7), 10, 3, "234567890", "555-1234", 1, 1, 1)
            };
        }
    }
}




