// GradeConfiguration.cs
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduardNikitinKT_31_23.Database.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    private const string TableName = "cd_grade";

    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.GradeId)
            .HasName("pk_cd_grade_grade_id");

        builder.Property(x => x.GradeId)
            .ValueGeneratedOnAdd()
            .HasColumnName("c_grade_id");

        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnName("c_grade_value");

        builder.Property(x => x.StudentId)
            .HasColumnName("f_student_id");

        builder.Property(x => x.DisciplineId)
            .HasColumnName("f_discipline_id");

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .HasConstraintName("fk_cd_grade_student")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Discipline)
            .WithMany()
            .HasForeignKey(x => x.DisciplineId)
            .HasConstraintName("fk_cd_grade_discipline")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.StudentId)
            .HasDatabaseName("idx_cd_grade_f_student_id");

        builder.HasIndex(x => x.DisciplineId)
            .HasDatabaseName("idx_cd_grade_f_discipline_id");

        builder.Navigation(x => x.Discipline).AutoInclude();
    }
}