// StudentConfiguration.cs
using EduardNikitinKT_31_23.Database.Helpers;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduardNikitinKT_31_23.Database.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    private const string TableName = "cd_student";

    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.StudentId)
            .HasName("pk_cd_student_student_id");

        builder.Property(x => x.StudentId)
            .ValueGeneratedOnAdd()
            .HasColumnName("c_student_id");

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasColumnName("c_student_firstname")
            .HasColumnType(ColumnType.String)
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasColumnName("c_student_lastname")
            .HasColumnType(ColumnType.String)
            .HasMaxLength(100);

        builder.Property(x => x.GroupId)
            .HasColumnName("f_group_id");

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasColumnName("c_is_deleted")
            .HasDefaultValue(false);

        builder.HasOne(x => x.Group)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.GroupId)
            .HasConstraintName("fk_cd_student_group")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.GroupId)
            .HasDatabaseName("idx_cd_student_f_group_id");

        builder.Navigation(x => x.Group).AutoInclude();
    }
}