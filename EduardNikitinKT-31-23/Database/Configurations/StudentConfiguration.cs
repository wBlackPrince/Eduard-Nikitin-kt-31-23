using EduardNikitinKT_31_23.Database.Helpers;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduardNikitinKT_31_23.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_student";

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(TableName);
            
            //Задаем первичный ключ
            builder
                .HasKey(p => p.StudentId)
                .HasName($"pk_{TableName}_student_id");

            //Для целочисленного первичного ключа задаем автогенерацию
            //(с каждой новой записи будет добавлять +1)
            builder.Property(p => p.StudentId)
                .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД,
            //а также их обязательность и тд
            builder.Property(p => p.StudentId)
                .HasColumnName("c_student_id")
                .HasComment("идентификатор студента");

            //HasComment добавит комментарий, который будет отображаться в СУБД
            //(добавлять по желанию)
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnName("c_student_firstname")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("имя студента");


                builder.Property(s => s.LastName)
                    .IsRequired()
                    .HasColumnName("c_student_lastname")
                    .HasColumnType(ColumnType.String)
                    .HasMaxLength(100);

                builder.Property(s => s.MiddleName)
                    .HasColumnName("c_student_middlename")
                    .HasColumnType(ColumnType.String)
                    .HasMaxLength(100);

                builder.Property(s => s.GroupId)
                    .HasColumnName("f_group_id");
            
            builder
                .HasOne(p => p.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(p => p.GroupId)
                .HasConstraintName("fk_f_group_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.GroupId, $"idx_{TableName}_fk_f_group_id");

            // Добавим явную автопогрузку связанной сущности
            builder.Navigation(p => p.Group)
                .AutoInclude();
        }
    }
}