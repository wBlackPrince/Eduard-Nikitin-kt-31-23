using EduardNikitinKT_31_23.Database.Helpers;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduardNikitinKT_31_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
             builder.ToTable(TableName);

            //Задаем первичный ключ
            builder
                .HasKey(p => p.GroupId)
                .HasName($"pk_{TableName}_group_id");

            //Для целочисленного первичного ключа задаем автогенерацию
            //(с каждой новой записи будет добавлять +1)
            builder.Property(p => p.GroupId)
                .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД,
            //а также их обязательность и тд
            builder.Property(p => p.GroupId)
                .HasColumnName("c_group_id")
                .HasComment("идентификатор группы");

            //HasComment добавит комментарий, который будет отображаться в СУБД
            //(добавлять по желанию)
            builder.Property(p => p.GroupName)
                .IsRequired()
                .HasColumnName("c_group_name")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("название группы");
        }
    }
}