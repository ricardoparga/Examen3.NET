using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen3.NET.Data.Migrations
{
    public partial class ModificacionEntrada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Precio",
                table: "Entradas",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "UrlImagen",
                table: "Entradas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImagen",
                table: "Entradas");

            migrationBuilder.AlterColumn<float>(
                name: "Precio",
                table: "Entradas",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
