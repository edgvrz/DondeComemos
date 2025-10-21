using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DondeComemos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDestacadoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Restaurantes",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<bool>(
                name: "Destacado",
                table: "Restaurantes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Restaurantes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Restaurantes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SitioWeb",
                table: "Restaurantes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Restaurantes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Alergenos",
                table: "Productos",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Calorias",
                table: "Productos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsVegano",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EsVegetariano",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Ingredientes",
                table: "Productos",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Picante",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecomendacionChef",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SinGluten",
                table: "Productos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TiempoPreparacion",
                table: "Productos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Mensaje = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Leida = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaLeida = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resenas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RestauranteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Comentario = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Calificacion = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    CalidadComida = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    Servicio = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    Ambiente = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    RelacionPrecio = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Verificado = table.Column<bool>(type: "INTEGER", nullable: false),
                    Aprobado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resenas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resenas_Restaurantes_RestauranteId",
                        column: x => x.RestauranteId,
                        principalTable: "Restaurantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resenas_RestauranteId",
                table: "Resenas",
                column: "RestauranteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Resenas");

            migrationBuilder.DropColumn(
                name: "Destacado",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "SitioWeb",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "Alergenos",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Calorias",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "EsVegano",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "EsVegetariano",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Ingredientes",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Orden",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Picante",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "RecomendacionChef",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "SinGluten",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "TiempoPreparacion",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Restaurantes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");
        }
    }
}
