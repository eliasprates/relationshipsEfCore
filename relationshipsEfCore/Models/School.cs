namespace relationshipsEfCore.Models
{
    public class School
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Principal { get; set; }
        public List<Teacher>? Teachers { get; set; }
        public List<Student>? Students { get; set; }
    }
}
