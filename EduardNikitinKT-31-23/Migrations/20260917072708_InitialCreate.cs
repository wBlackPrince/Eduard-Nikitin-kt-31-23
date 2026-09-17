using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EduardNikitinKT_31_23.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_f_group_id",
                table: "cd_student");

            migrationBuilder.DropColumn(
                name: "c_student_middlename",
                table: "cd_student");

            migrationBuilder.RenameIndex(
                name: "idx_cd_student_fk_f_group_id",
                table: "cd_student",
                newName: "idx_cd_student_f_group_id");

            migrationBuilder.AlterColumn<string>(
                name: "c_student_firstname",
                table: "cd_student",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100,
                oldComment: "имя студента");

            migrationBuilder.AlterColumn<int>(
                name: "c_student_id",
                table: "cd_student",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "идентификатор студента")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "c_is_deleted",
                table: "cd_student",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "c_group_name",
                table: "cd_group",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100,
                oldComment: "название группы");

            migrationBuilder.AlterColumn<int>(
                name: "c_group_id",
                table: "cd_group",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "идентификатор группы")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "c_course",
                table: "cd_group",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "f_speciality_id",
                table: "cd_group",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "cd_discipline",
                columns: table => new
                {
                    c_discipline_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_discipline_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    c_is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_discipline_id", x => x.c_discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_speciality",
                columns: table => new
                {
                    c_speciality_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_speciality_title = table.Column<string>(type: "varchar", maxLength: 150, nullable: false),
                    c_speciality_code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_speciality_speciality_id", x => x.c_speciality_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_grade",
                columns: table => new
                {
                    c_grade_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_grade_value = table.Column<int>(type: "integer", nullable: false),
                    f_student_id = table.Column<int>(type: "integer", nullable: false),
                    f_discipline_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_grade_grade_id", x => x.c_grade_id);
                    table.ForeignKey(
                        name: "fk_cd_grade_discipline",
                        column: x => x.f_discipline_id,
                        principalTable: "cd_discipline",
                        principalColumn: "c_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cd_grade_student",
                        column: x => x.f_student_id,
                        principalTable: "cd_student",
                        principalColumn: "c_student_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_group_f_speciality_id",
                table: "cd_group",
                column: "f_speciality_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_f_discipline_id",
                table: "cd_grade",
                column: "f_discipline_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_f_student_id",
                table: "cd_grade",
                column: "f_student_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_speciality_code",
                table: "cd_speciality",
                column: "c_speciality_code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_cd_group_speciality",
                table: "cd_group",
                column: "f_speciality_id",
                principalTable: "cd_speciality",
                principalColumn: "c_speciality_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_cd_student_group",
                table: "cd_student",
                column: "f_group_id",
                principalTable: "cd_group",
                principalColumn: "c_group_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cd_group_speciality",
                table: "cd_group");

            migrationBuilder.DropForeignKey(
                name: "fk_cd_student_group",
                table: "cd_student");

            migrationBuilder.DropTable(
                name: "cd_grade");

            migrationBuilder.DropTable(
                name: "cd_speciality");

            migrationBuilder.DropTable(
                name: "cd_discipline");

            migrationBuilder.DropIndex(
                name: "idx_cd_group_f_speciality_id",
                table: "cd_group");

            migrationBuilder.DropColumn(
                name: "c_is_deleted",
                table: "cd_student");

            migrationBuilder.DropColumn(
                name: "c_course",
                table: "cd_group");

            migrationBuilder.DropColumn(
                name: "f_speciality_id",
                table: "cd_group");

            migrationBuilder.RenameIndex(
                name: "idx_cd_student_f_group_id",
                table: "cd_student",
                newName: "idx_cd_student_fk_f_group_id");

            migrationBuilder.AlterColumn<string>(
                name: "c_student_firstname",
                table: "cd_student",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                comment: "имя студента",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "c_student_id",
                table: "cd_student",
                type: "integer",
                nullable: false,
                comment: "идентификатор студента",
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "c_student_middlename",
                table: "cd_student",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "c_group_name",
                table: "cd_group",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                comment: "название группы",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "c_group_id",
                table: "cd_group",
                type: "integer",
                nullable: false,
                comment: "идентификатор группы",
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "fk_f_group_id",
                table: "cd_student",
                column: "f_group_id",
                principalTable: "cd_group",
                principalColumn: "c_group_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
