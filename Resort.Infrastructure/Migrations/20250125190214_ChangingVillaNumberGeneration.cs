using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangingVillaNumberGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_VillaNumbers",
                table: "VillaNumbers");

            // Step 2: Drop the VillaNo column
            migrationBuilder.DropColumn(
                name: "VillaNo",
                table: "VillaNumbers");

            // Step 3: Recreate the VillaNo column with the IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "VillaNo",
                table: "VillaNumbers",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Step 4: Recreate the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_VillaNumbers",
                table: "VillaNumbers",
                column: "VillaNo");

            // Update data in the Villas table (unchanged)
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 25, 21, 2, 13, 620, DateTimeKind.Local).AddTicks(8113), new DateTime(2025, 1, 25, 21, 2, 13, 620, DateTimeKind.Local).AddTicks(8153) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 25, 21, 2, 13, 620, DateTimeKind.Local).AddTicks(8161), new DateTime(2025, 1, 25, 21, 2, 13, 620, DateTimeKind.Local).AddTicks(8162) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 25, 21, 2, 13, 620, DateTimeKind.Local).AddTicks(8170), new DateTime(2025, 1, 25, 21, 2, 13, 620, DateTimeKind.Local).AddTicks(8165) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 25, 21, 2, 13, 620, DateTimeKind.Local).AddTicks(8174), new DateTime(2025, 1, 25, 21, 2, 13, 620, DateTimeKind.Local).AddTicks(8173) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_VillaNumbers",
                table: "VillaNumbers");

            // Step 2: Drop the VillaNo column
            migrationBuilder.DropColumn(
                name: "VillaNo",
                table: "VillaNumbers");

            // Step 3: Recreate the VillaNo column without the IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "VillaNo",
                table: "VillaNumbers",
                type: "int",
                nullable: false);

            // Step 4: Recreate the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_VillaNumbers",
                table: "VillaNumbers",
                column: "VillaNo");

            // Update data in the Villas table (unchanged)
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
        }
    }
}