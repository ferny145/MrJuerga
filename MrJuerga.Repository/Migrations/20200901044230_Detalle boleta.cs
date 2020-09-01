using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MrJuerga.Repository.Migrations
{
    public partial class Detalleboleta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Precio",
                table: "Productos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DetalleBoletas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductoId = table.Column<int>(nullable: false),
                    BoletaId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Subtotal = table.Column<float>(nullable: false),
                    PaqueteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleBoletas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleBoletas_Boletas_BoletaId",
                        column: x => x.BoletaId,
                        principalTable: "Boletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleBoletas_Paquetes_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleBoletas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleBoletas_BoletaId",
                table: "DetalleBoletas",
                column: "BoletaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleBoletas_PaqueteId",
                table: "DetalleBoletas",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleBoletas_ProductoId",
                table: "DetalleBoletas",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleBoletas");

            migrationBuilder.AlterColumn<string>(
                name: "Precio",
                table: "Productos",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
