using EduardNikitinKT_31_23.Database.Configurations;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace EduardNikitinKT_31_23.db
{
    public class StudentDbContext : DbContext
    {
        // Добавляем таблицы
        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Speciality> Specialities { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Добавляем конфигурации
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentDbContext).Assembly);
        }

        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options)
        {
        }
    }
}
