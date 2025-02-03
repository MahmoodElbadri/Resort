using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Resort.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    Sqft = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Capacity", "CreatedDate", "Description", "ImageUrl", "Name", "Occupancy", "Price", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2025, 2, 3, 19, 33, 22, 679, DateTimeKind.Local).AddTicks(1695), "This is details of Villa 1", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.independent.co.uk%2Ftravel%2Fvilla-holidays-europe-spain-greece-portugal-cyprus-b2472690.html&psig=AOvVaw25JOKvkOKym5g2kAZYqZ9O&ust=1737838204750000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCOCdp_mdj4sDFQAAAAAdAAAAABAE", "Royal Villa", 4, 200.0, 300, new DateTime(2025, 2, 3, 19, 33, 22, 679, DateTimeKind.Local).AddTicks(1761) },
                    { 2, 0, new DateTime(2025, 2, 3, 19, 33, 22, 679, DateTimeKind.Local).AddTicks(1766), "This is details of Villa 2", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.villaplus.com%2Fbest-villa-holidays&psig=AOvVaw25JOKvkOKym5g2kAZYqZ9O&ust=1737838204750000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCOCdp_mdj4sDFQAAAAAdAAAAABAJ", "Palace Villa", 4, 150.0, 200, new DateTime(2025, 2, 3, 19, 33, 22, 679, DateTimeKind.Local).AddTicks(1769) },
                    { 3, 0, new DateTime(2025, 2, 3, 19, 33, 22, 679, DateTimeKind.Local).AddTicks(1776), "This is details of Villa 3", "https://www.google.com/imgres?imgurl=https%3A%2F%2Fwww.rentvillasalgarve.co.uk%2Fmedia%2F2418546%2FLuxury-holiday-villa-rental-Quinta-do-Lago.jpg%3Fwidth%3D600%26height%3D420%26scale%3Dboth%26mode%3Dcrop%26quality%3D75&tbnid=2pgayn9pyvB0yM&vet=10CBIQxiAoAmoXChMI4J2n-Z2PiwMVAAAAAB0AAAAAEA8..i&imgrefurl=https%3A%2F%2Fwww.rentvillasalgarve.co.uk%2F&docid=TF4EoHH-oNiY2M&w=600&h=420&itg=1&q=villas&ved=0CBIQxiAoAmoXChMI4J2n-Z2PiwMVAAAAAB0AAAAAEA8", "Paradise Villa", 4, 600.0, 100, new DateTime(2025, 2, 3, 19, 33, 22, 679, DateTimeKind.Local).AddTicks(1773) },
                    { 4, 0, new DateTime(2025, 2, 3, 19, 33, 22, 679, DateTimeKind.Local).AddTicks(1783), "This is details of Villa 4", "https://www.google.com/imgres?imgurl=https%3A%2F%2Fvillaimages.villaplus.com%2Fimages%2Fvillas%2Fphotos%2F8bb14538-2835-41ba-9c8c-e739fd8069a5_725.jpg&tbnid=H7579zpt_96g-M&vet=10CBYQxiAoCGoXChMI4J2n-Z2PiwMVAAAAAB0AAAAAEA8..i&imgrefurl=https%3A%2F%2Fwww.villaplus.com%2Fdestinations%2Fvillas-in-spain%2Fvillas-in-balearic-islands%2Fmenorca%2Fcalan-porter&docid=t6qGL44sGUmn4M&w=725&h=482&itg=1&q=villas&ved=0CBYQxiAoCGoXChMI4J2n-Z2PiwMVAAAAAB0AAAAAEA8", "Luxury Villa", 4, 700.0, 120, new DateTime(2025, 2, 3, 19, 33, 22, 679, DateTimeKind.Local).AddTicks(1780) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}
