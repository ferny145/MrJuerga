using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MrJuerga.Repository.Migrations
{
    public partial class actualizaciónpaquete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleBoletas_Paquetes_PaqueteId",
                table: "DetalleBoletas");

            migrationBuilder.DropTable(
                name: "ProductoPaquetes");

            migrationBuilder.DropTable(
                name: "Paquetes");

            migrationBuilder.DropIndex(
                name: "IX_DetalleBoletas_PaqueteId",
                table: "DetalleBoletas");

            migrationBuilder.DropColumn(
                name: "CantidadPaquete",
                table: "DetalleBoletas");

            migrationBuilder.DropColumn(
                name: "CantidadProducto",
                table: "DetalleBoletas");

            migrationBuilder.RenameColumn(
                name: "PaqueteId",
                table: "DetalleBoletas",
                newName: "Cantidad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "DetalleBoletas",
                newName: "PaqueteId");

            migrationBuilder.AddColumn<int>(
                name: "CantidadPaquete",
                table: "DetalleBoletas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CantidadProducto",
                table: "DetalleBoletas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Paquetes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Precio = table.Column<float>(nullable: false),
                    Stock = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquetes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductoPaquetes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaqueteId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoPaquetes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoPaquetes_Paquetes_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoPaquetes_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleBoletas_PaqueteId",
                table: "DetalleBoletas",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoPaquetes_PaqueteId",
                table: "ProductoPaquetes",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoPaquetes_ProductoId",
                table: "ProductoPaquetes",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleBoletas_Paquetes_PaqueteId",
                table: "DetalleBoletas",
                column: "PaqueteId",
                principalTable: "Paquetes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
