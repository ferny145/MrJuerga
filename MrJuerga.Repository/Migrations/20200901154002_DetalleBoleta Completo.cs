using Microsoft.EntityFrameworkCore.Migrations;

namespace MrJuerga.Repository.Migrations
{
    public partial class DetalleBoletaCompleto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "DetalleBoletas",
                newName: "CantidadProducto");

            migrationBuilder.AlterColumn<float>(
                name: "Precio",
                table: "Paquetes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadPaquete",
                table: "DetalleBoletas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadPaquete",
                table: "DetalleBoletas");

            migrationBuilder.RenameColumn(
                name: "CantidadProducto",
                table: "DetalleBoletas",
                newName: "Cantidad");

            migrationBuilder.AlterColumn<string>(
                name: "Precio",
                table: "Paquetes",
                nullable: true,
                oldClrType: typeof(float));
        }
    }
}
