using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Resort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingAmenityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VillaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenities_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Description", "Name", "VillaId" },
                values: new object[,]
                {
                    { 1, "This is details of Amenity 1", "Air conditioning", 3 },
                    { 2, "This is details of Amenity 2", "Microwave", 15 },
                    { 3, "This is details of Amenity 3", "Refrigerator", 15 },
                    { 4, "This is details of Amenity 4", "Oven", 15 }
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 7, 22, 29, 29, 680, DateTimeKind.Local).AddTicks(3025), new DateTime(2025, 2, 7, 22, 29, 29, 680, DateTimeKind.Local).AddTicks(3093) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 7, 22, 29, 29, 680, DateTimeKind.Local).AddTicks(3100), new DateTime(2025, 2, 7, 22, 29, 29, 680, DateTimeKind.Local).AddTicks(3103) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 7, 22, 29, 29, 680, DateTimeKind.Local).AddTicks(3111), new DateTime(2025, 2, 7, 22, 29, 29, 680, DateTimeKind.Local).AddTicks(3108) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 7, 22, 29, 29, 680, DateTimeKind.Local).AddTicks(3119), new DateTime(2025, 2, 7, 22, 29, 29, 680, DateTimeKind.Local).AddTicks(3116) });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_VillaId",
                table: "Amenities",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 3, 22, 15, 39, 714, DateTimeKind.Local).AddTicks(6178), new DateTime(2025, 2, 3, 22, 15, 39, 714, DateTimeKind.Local).AddTicks(6228) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 3, 22, 15, 39, 714, DateTimeKind.Local).AddTicks(6232), new DateTime(2025, 2, 3, 22, 15, 39, 714, DateTimeKind.Local).AddTicks(6234) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 3, 22, 15, 39, 714, DateTimeKind.Local).AddTicks(6239), new DateTime(2025, 2, 3, 22, 15, 39, 714, DateTimeKind.Local).AddTicks(6237) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 3, 22, 15, 39, 714, DateTimeKind.Local).AddTicks(6243), new DateTime(2025, 2, 3, 22, 15, 39, 714, DateTimeKind.Local).AddTicks(6241) });
        }
    }
}
