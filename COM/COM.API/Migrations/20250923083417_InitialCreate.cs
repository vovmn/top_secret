using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COM.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "construction_objects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    address = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    polygon = table.Column<string>(type: "geometry(Polygon, 4326)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_construction_objects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "checklists",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    construction_object_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    file_id = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    content = table.Column<string>(type: "JSONB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checklists", x => x.id);
                    table.ForeignKey(
                        name: "FK_checklists_construction_objects_construction_object_id",
                        column: x => x.construction_object_id,
                        principalTable: "construction_objects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "object_responsibles",
                columns: table => new
                {
                    construction_object_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_object_responsibles", x => new { x.construction_object_id, x.user_id, x.role });
                    table.ForeignKey(
                        name: "FK_object_responsibles_construction_objects_construction_objec~",
                        column: x => x.construction_object_id,
                        principalTable: "construction_objects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checklists_construction_object_id",
                table: "checklists",
                column: "construction_object_id");

            migrationBuilder.CreateIndex(
                name: "IX_construction_objects_status",
                table: "construction_objects",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checklists");

            migrationBuilder.DropTable(
                name: "object_responsibles");

            migrationBuilder.DropTable(
                name: "construction_objects");
        }
    }
}
