// GroupConfiguration.cs
using EduardNikitinKT_31_23.Database.Helpers;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduardNikitinKT_31_23.Database.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    private const string TableName = "cd_group";

    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.GroupId)
            .HasName("pk_cd_group_group_id");

        builder.Property(x => x.GroupId)
            .ValueGeneratedOnAdd()
            .HasColumnName("c_group_id");

        builder.Property(x => x.GroupName)
            .IsRequired()
            .HasColumnName("c_group_name")
            .HasColumnType(ColumnType.String)
            .HasMaxLength(100);

        builder.Property(x => x.Course)
            .IsRequired()
            .HasColumnName("c_course");

        builder.Property(x => x.SpecialityId)
            .HasColumnName("f_speciality_id");

        builder.HasOne<Speciality>()
            .WithMany()
            .HasForeignKey(x => x.SpecialityId)
            .HasConstraintName("fk_cd_group_speciality")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.SpecialityId)
            .HasDatabaseName("idx_cd_group_f_speciality_id");
    }
}