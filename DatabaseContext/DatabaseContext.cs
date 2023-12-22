using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext;
// контекст базы данных и файлы миграций
// Чтобы подключиться к базе данных через Entity Framework, нам нужен контекст данных.
// Контекст данных представляет собой класс, производный от класса DbContext.
// Контекст данных содержит одно или несколько свойств типа DbSet<T>, где T представляет тип объекта,
// хранящегося в базе данных. 
// мы получаем доступ к данным соответствующих моделей, которые хранятся в базе данных
public class DataBaseContext : DbContext
{
    public DbSet<Doctor>? Doctors { get; set; } = null;
    public DbSet<Specialization>? Specializations { get; set; } = null;
    public DbSet<Certificate>? Certificates { get; set; } = null;
    private const string sqlServer = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=MarsArina";
    
    public DataBaseContext()
    {
        Database.EnsureCreated();        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(sqlServer);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasKey(d => d.ID);
        modelBuilder.Entity<Doctor>().Property(d => d.Name).IsRequired(true);
        modelBuilder.Entity<Doctor>().Property(d => d.SpecializationName).IsRequired(true);
        modelBuilder.Entity<Specialization>().HasKey(s => s.ID);
        modelBuilder.Entity<Specialization>().Property(s => s.Name).IsRequired(true);
        modelBuilder.Entity<Certificate>().HasKey(c => c.ID);
        modelBuilder.Entity<Certificate>().Property(c => c.Description).IsRequired(true);
        modelBuilder.Entity<Certificate>().Property(c => c.Date).IsRequired(true);
        modelBuilder.Entity<Certificate>().Property(c => c.DoctorName).IsRequired(true);
        modelBuilder.Entity<Specialization>().HasMany<Doctor>().WithOne(d => d.Specialization)
            .IsRequired(true).HasForeignKey(doc => doc.SpecializationID);
        modelBuilder.Entity<Doctor>().HasMany<Certificate>().WithOne(c => c.Doctor)
            .IsRequired(true).HasForeignKey(cert => cert.DoctorID);
        base.OnModelCreating(modelBuilder);
    }
    
    
}