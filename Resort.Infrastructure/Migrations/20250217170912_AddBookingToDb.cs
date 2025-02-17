using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    Nights = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckOutDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsPaymentSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StripeSessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualCheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VillaNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 17, 19, 9, 8, 411, DateTimeKind.Local).AddTicks(4817), new DateTime(2025, 2, 17, 19, 9, 8, 411, DateTimeKind.Local).AddTicks(4942) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 17, 19, 9, 8, 411, DateTimeKind.Local).AddTicks(4962), new DateTime(2025, 2, 17, 19, 9, 8, 411, DateTimeKind.Local).AddTicks(4969) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 17, 19, 9, 8, 411, DateTimeKind.Local).AddTicks(4981), new DateTime(2025, 2, 17, 19, 9, 8, 411, DateTimeKind.Local).AddTicks(4988) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 2, 17, 19, 9, 8, 411, DateTimeKind.Local).AddTicks(4999), new DateTime(2025, 2, 17, 19, 9, 8, 411, DateTimeKind.Local).AddTicks(5005) });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VillaId",
                table: "Bookings",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

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
    }
}
