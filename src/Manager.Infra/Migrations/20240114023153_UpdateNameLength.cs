using Microsoft.EntityFrameworkCore.Migrations;

namespace Manager.Infra.Migrations
{
    public partial class UpdateNameLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "User",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)",
                oldMaxLength: 80)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "User",
                type: "VARCHAR(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
