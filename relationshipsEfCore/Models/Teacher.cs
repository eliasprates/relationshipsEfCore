namespace relationshipsEfCore.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public List<School>? Schools { get; set; }
        public List<Student>? Students { get; set; }
    }
}
