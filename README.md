
# Mapeamento de Relacionamentos no Entity Framework Core

Este documento fornece uma visão geral de como mapear relacionamentos entre entidades usando o Entity Framework Core. Utilizando as entidades `Enrollment`, `School`, `Student`, e `Teacher` como exemplos, demonstramos diferentes tipos de relacionamentos: um-para-um, um-para-muitos e muitos-para-muitos.

## Entidades

Aqui estão as entidades básicas utilizadas:

### `Enrollment`

```csharp
public class Enrollment
{
    public int Id { get; set; }
    public int EnrollmentNumber { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
```

### `School`

```csharp
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
```

### `Student`

```csharp
public class Student
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public int EnrollmentId { get; set; }
    public int SchoolId { get; set; }
    public Enrollment? Enrollment { get; set; }
    public School? School { get; set; }
    public List<Teacher>? Teachers { get; set; }
}
```

### `Teacher`

```csharp
public class Teacher
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Subject { get; set; }
    public List<School>? Schools { get; set; }
    public List<Student>? Students { get; set; }
}
```

## Configuração de DbContext

A classe `ApplicationDbContext` é configurada com `DbSet` para cada uma das entidades e o mapeamento de relacionamentos é feito usando Fluent API no método `OnModelCreating`.

### Exemplo de `ApplicationDbContext`

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações dos modelos e seus relacionamentos
        // ...
    }
}
```

## Mapeamento de Relacionamentos

### Relacionamentos Um-para-Um

- **Student - Enrollment:** Cada estudante (`Student`) possui um registro de matrícula (`Enrollment`).

### Relacionamentos Um-para-Muitos

- **School - Student:** Uma escola (`School`) pode ter vários estudantes (`Student`).

### Relacionamentos Muitos-para-Muitos

- **School - Teacher:** Uma escola (`School`) pode ter vários professores (`Teacher`), e um professor (`Teacher`) pode ensinar em várias escolas (`School`).
- **Student - Teacher:** Um estudante (`Student`) pode ser ensinado por vários professores (`Teacher`), e um professor (`Teacher`) pode ensinar vários estudantes (`Student`).

## Considerações Finais

Este README fornece um guia básico para o mapeamento de relacionamentos usando o Entity Framework Core. Para cada tipo de relacionamento, é crucial entender como configurar corretamente as entidades e suas relações para garantir um modelo de dados eficiente e eficaz.

## `ApplicationDbContext` e Mapeamento no `OnModelCreating`

A classe `ApplicationDbContext` é o ponto central para configurar as regras de mapeamento das entidades para o banco de dados. No método `OnModelCreating`, utilizamos a Fluent API para definir detalhadamente como as entidades são mapeadas, incluindo o tipo de relacionamentos entre elas.

### Exemplo Detalhado de `OnModelCreating`

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Relacionamento Um-Para-Um: Student - Enrollment
    modelBuilder.Entity<Student>()
        .HasOne(s => s.Enrollment)
        .WithOne()
        .HasForeignKey<Student>(s => s.EnrollmentId);

    // Relacionamento Um-Para-Muitos: School - Student
    modelBuilder.Entity<School>()
        .HasMany(s => s.Students)
        .WithOne(s => s.School)
        .HasForeignKey(s => s.SchoolId);

    // Relacionamento Muitos-Para-Muitos: School - Teacher
    modelBuilder.Entity<School>()
        .HasMany(s => s.Teachers)
        .WithMany(t => t.Schools);

    // Relacionamento Muitos-Para-Muitos: Student - Teacher
    modelBuilder.Entity<Student>()
        .HasMany(s => s.Teachers)
        .WithMany(t => t.Students);

    // Outras configurações...
}
```

Cada método na Fluent API é usado para especificar um aspecto diferente do mapeamento:

- `HasKey`: Define a chave primária da entidade.
- `HasMany`/`WithOne`/`WithMany`: Define os tipos de relacionamentos entre as entidades.
- `HasForeignKey`: Especifica a chave estrangeira em um relacionamento.
- `Property`: Usado para configurar aspectos das propriedades, como tamanho máximo.

Este mapeamento assegura que o EF Core possa traduzir corretamente suas operações em comandos de banco de dados que respeitam as relações entre as entidades.

## Considerações Finais

Este guia oferece um panorama de como configurar e mapear relacionamentos no Entity Framework Core. Estas configurações são fundamentais para um aproveitamento eficaz do EF Core e para garantir a integridade dos dados em seu banco de dados.
