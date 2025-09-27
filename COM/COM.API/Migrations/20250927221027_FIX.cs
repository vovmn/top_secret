using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COM.API.Migrations
{
    /// <inheritdoc />
    public partial class FIX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "polygon",
                table: "construction_objects",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "geometry(Polygon, 4326)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "polygon",
                table: "construction_objects",
                type: "geometry(Polygon, 4326)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
