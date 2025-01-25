using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Resort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumbersSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumbers", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_VillaNumbers_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VillaNumbers",
                columns: new[] { "VillaNo", "SpecialDetails", "VillaId" },
                values: new object[,]
                {
                    { 1, null, 1 },
                    { 2, null, 1 },
                    { 3, null, 2 },
                    { 4, null, 3 },
                    { 5, null, 3 },
                    { 6, null, 4 },
                    { 7, null, 4 }
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 25, 16, 15, 14, 871, DateTimeKind.Local).AddTicks(3770), new DateTime(2025, 1, 25, 16, 15, 14, 871, DateTimeKind.Local).AddTicks(3816) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 25, 16, 15, 14, 871, DateTimeKind.Local).AddTicks(3820), new DateTime(2025, 1, 25, 16, 15, 14, 871, DateTimeKind.Local).AddTicks(3822) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 25, 16, 15, 14, 871, DateTimeKind.Local).AddTicks(3827), new DateTime(2025, 1, 25, 16, 15, 14, 871, DateTimeKind.Local).AddTicks(3825) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 25, 16, 15, 14, 871, DateTimeKind.Local).AddTicks(3831), new DateTime(2025, 1, 25, 16, 15, 14, 871, DateTimeKind.Local).AddTicks(3830) });

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 24, 23, 36, 10, 838, DateTimeKind.Local).AddTicks(1052), new DateTime(2025, 1, 24, 23, 36, 10, 838, DateTimeKind.Local).AddTicks(1219) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 24, 23, 36, 10, 838, DateTimeKind.Local).AddTicks(1224), new DateTime(2025, 1, 24, 23, 36, 10, 838, DateTimeKind.Local).AddTicks(1226) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 24, 23, 36, 10, 838, DateTimeKind.Local).AddTicks(1231), new DateTime(2025, 1, 24, 23, 36, 10, 838, DateTimeKind.Local).AddTicks(1229) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 24, 23, 36, 10, 838, DateTimeKind.Local).AddTicks(1236), new DateTime(2025, 1, 24, 23, 36, 10, 838, DateTimeKind.Local).AddTicks(1234) });
        }
    }
}
