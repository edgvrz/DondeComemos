using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DondeComemos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoApellidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "UserProfiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "UserProfiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "UserProfiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistorialBusquedas",
                table: "UserProfiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "UserProfiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RestaurantesFavoritos",
                table: "UserProfiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaActividad",
                table: "UserProfiles",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "HistorialBusquedas",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "RestaurantesFavoritos",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UltimaActividad",
                table: "UserProfiles");
        }
    }
}
