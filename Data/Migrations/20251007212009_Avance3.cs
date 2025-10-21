using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DondeComemos.Data.Migrations
{
    /// <inheritdoc />
    public partial class Avance3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Horario",
                table: "Restaurantes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitud",
                table: "Restaurantes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitud",
                table: "Restaurantes",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RangoPrecios",
                table: "Restaurantes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Restaurantes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCocina",
                table: "Restaurantes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false),
                    Disponible = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImagenUrl = table.Column<string>(type: "TEXT", nullable: true),
                    RestauranteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Restaurantes_RestauranteId",
                        column: x => x.RestauranteId,
                        principalTable: "Restaurantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_RestauranteId",
                table: "Productos",
                column: "RestauranteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropColumn(
                name: "Horario",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "RangoPrecios",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "TipoCocina",
                table: "Restaurantes");
        }
    }
}
