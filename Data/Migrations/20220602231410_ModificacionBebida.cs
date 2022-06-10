using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen3.NET.Data.Migrations
{
    public partial class ModificacionBebida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Precio",
                table: "Bebidas",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "UrlImagen",
                table: "Bebidas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImagen",
                table: "Bebidas");

            migrationBuilder.AlterColumn<float>(
                name: "Precio",
                table: "Bebidas",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
