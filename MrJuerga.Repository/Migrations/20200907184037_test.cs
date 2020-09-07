using Microsoft.EntityFrameworkCore.Migrations;

namespace MrJuerga.Repository.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dni",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "Genero",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Dni",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Usuarios",
                nullable: true);
        }
    }
}
