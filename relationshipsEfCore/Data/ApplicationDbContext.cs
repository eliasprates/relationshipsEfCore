using Microsoft.EntityFrameworkCore;
using relationshipsEfCore.Models;

namespace relationshipsEfCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<School> Schools { get; set;}
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária // Primary Key
                entity.Property(e => e.EnrollmentNumber).IsRequired(); // Campo obrigatório // Required field
                entity.Property(e => e.EnrollmentDate).HasColumnType("datetime"); // Especificando o tipo // Specifying the data type
            });


            modelBuilder.Entity<School>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária // Primary Key
                entity.Property(e => e.Phone).HasMaxLength(15); // Tamanho Máximo // Maximum length
                entity.Property(e => e.Name).HasMaxLength(100); // Tamanho Máximo // Maximum length
                entity.Property(e => e.Principal).HasMaxLength(100); // Tamanho Máximo // Maximum length
                entity.Property(e => e.Address).HasMaxLength(200); // Tamanho Máximo // Maximum length

                // Relação Muitos-Para-Muitos com Teacher
                // Many-to-Many relationship with Teacher
                entity.HasMany(s => s.Teachers).WithMany(t => t.Schools);

                // Relação Um-Para-Muitos com Student
                // One-to-Many relationship with Student
                entity.HasMany(s => s.Students).WithOne(s => s.School).HasForeignKey(s => s.SchoolId);
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária // Primary Key
                entity.Property(e => e.Name).HasMaxLength(100); // Tamanho Máximo // Maximum length
                entity.Property(e => e.Address).HasMaxLength(200); // Tamanho Máximo // Maximum length
                entity.Property(e => e.Phone).HasMaxLength(15); // Tamanho Máximo // Maximum length
                entity.Property(e => e.Principal).HasMaxLength(100); // Tamanho Máximo // Maximum length

                // Relação Muitos-Para-Muitos com Teacher
                // Many-to-Many relationship with Teacher
                entity.HasMany(s => s.Teachers).WithMany(t => t.Schools);

                // Relação Um-Para-Muitos com Student
                // One-to-Many relationship with Student
                entity.HasMany(s => s.Students).WithOne(s => s.School).HasForeignKey(s => s.SchoolId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária // Primary Key
                entity.Property(e => e.Name).HasMaxLength(100); // Tamanho Máximo // Maximum length
                entity.Property(e => e.Age).HasColumnType("int"); // Especificando o tipo // Specifying the data type

                // Relação Um-Para-Um com Enrollment
                // One-to-One relationship with Enrollment
                entity.HasOne(s => s.Enrollment).WithOne().HasForeignKey<Student>(s => s.EnrrolmentId);

                // Relação Um-Para-Muitos com School
                // One-to-Many relationship with School
                entity.HasOne(s => s.School).WithMany(s => s.Students).HasForeignKey(s => s.SchoolId);

                // Relação Muitos-Para-Muitos com Teacher
                // Many-to-Many relationship with Teacher
                entity.HasMany(s => s.Teachers).WithMany(t => t.Students);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária // Primary Key
                entity.Property(e => e.Name).HasMaxLength(100); // Tamanho Máximo // Maximum length
                entity.Property(e => e.Subject).HasMaxLength(100); // Tamanho Máximo // Maximum length

                // Relação Muitos-Para-Muitos com School
                // Many-to-Many relationship with School
                entity.HasMany(t => t.Schools).WithMany(s => s.Teachers);

                // Relação Muitos-Para-Muitos com Student
                // Many-to-Many relationship with Student
                entity.HasMany(t => t.Students).WithMany(s => s.Teachers);
            });


        }

    }
}
