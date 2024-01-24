namespace relationshipsEfCore.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public int EnrrolmentId { get; set; }
        public int SchoolId { get; set; }
        public Enrollment? Enrollment { get; set; }
        public School? School { get; set; }
        public List<Teacher>? Teachers { get; set; }
    }
}
