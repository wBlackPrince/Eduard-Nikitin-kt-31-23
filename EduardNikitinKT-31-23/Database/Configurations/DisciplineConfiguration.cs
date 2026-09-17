// DisciplineConfiguration.cs
using EduardNikitinKT_31_23.Database.Helpers;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduardNikitinKT_31_23.Database.Configurations;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    private const string TableName = "cd_discipline";

    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.DisciplineId)
            .HasName("pk_cd_discipline_discipline_id");

        builder.Property(x => x.DisciplineId)
            .ValueGeneratedOnAdd()
            .HasColumnName("c_discipline_id");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("c_discipline_name")
            .HasColumnType(ColumnType.String)
            .HasMaxLength(100);

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasColumnName("c_is_deleted")
            .HasDefaultValue(false);
    }
}