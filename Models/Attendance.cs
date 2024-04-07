namespace WebFormL1.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool Attended { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
