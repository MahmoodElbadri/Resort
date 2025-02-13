using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingPropertiesToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 13, 16, 27, 29, 867, DateTimeKind.Local).AddTicks(7187), new DateTime(2025, 2, 13, 16, 27, 29, 867, DateTimeKind.Local).AddTicks(7268) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 13, 16, 27, 29, 867, DateTimeKind.Local).AddTicks(7277), new DateTime(2025, 2, 13, 16, 27, 29, 867, DateTimeKind.Local).AddTicks(7281) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 13, 16, 27, 29, 867, DateTimeKind.Local).AddTicks(7288), new DateTime(2025, 2, 13, 16, 27, 29, 867, DateTimeKind.Local).AddTicks(7292) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 13, 16, 27, 29, 867, DateTimeKind.Local).AddTicks(7298), new DateTime(2025, 2, 13, 16, 27, 29, 867, DateTimeKind.Local).AddTicks(7302) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 13, 16, 12, 31, 674, DateTimeKind.Local).AddTicks(2311), new DateTime(2025, 2, 13, 16, 12, 31, 674, DateTimeKind.Local).AddTicks(2409) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 13, 16, 12, 31, 674, DateTimeKind.Local).AddTicks(2424), new DateTime(2025, 2, 13, 16, 12, 31, 674, DateTimeKind.Local).AddTicks(2428) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 13, 16, 12, 31, 674, DateTimeKind.Local).AddTicks(2437), new DateTime(2025, 2, 13, 16, 12, 31, 674, DateTimeKind.Local).AddTicks(2442) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 13, 16, 12, 31, 674, DateTimeKind.Local).AddTicks(2449), new DateTime(2025, 2, 13, 16, 12, 31, 674, DateTimeKind.Local).AddTicks(2454) });
        }
    }
}
