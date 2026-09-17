// SpecialityConfiguration.cs
using EduardNikitinKT_31_23.Database.Helpers;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduardNikitinKT_31_23.Database.Configurations;

public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
{
    private const string TableName = "cd_speciality";

    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.SpecialityId)
            .HasName("pk_cd_speciality_speciality_id");

        builder.Property(x => x.SpecialityId)
            .ValueGeneratedOnAdd()
            .HasColumnName("c_speciality_id");

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("c_speciality_title")
            .HasColumnType(ColumnType.String)
            .HasMaxLength(150);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnName("c_speciality_code");

        builder.HasIndex(x => x.Code)
            .IsUnique()
            .HasDatabaseName("idx_cd_speciality_code");
    }
}