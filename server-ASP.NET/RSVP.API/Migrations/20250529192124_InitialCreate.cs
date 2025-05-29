using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RSVP.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.UniqueConstraint("AK_Services_ServiceId", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StoreId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.UniqueConstraint("AK_Stores_StoreId", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Admin"),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    LastLoginAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StoreId = table.Column<string>(type: "TEXT", nullable: false),
                    ServiceId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CustomerPhone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReservationTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    AgreedToTerms = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StoreId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreHours_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreServices",
                columns: table => new
                {
                    StoreId = table.Column<string>(type: "TEXT", nullable: false),
                    ServiceId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreServices", x => new { x.StoreId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_StoreServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreServices_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegularHour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StoreHourId = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    Open = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Close = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    IsClosed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegularHour_StoreHours_StoreHourId",
                        column: x => x.StoreHourId,
                        principalTable: "StoreHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Open = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Close = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    IsClosed = table.Column<bool>(type: "INTEGER", nullable: false),
                    StoreHourId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialDate_StoreHours_StoreHourId",
                        column: x => x.StoreHourId,
                        principalTable: "StoreHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "Duration", "Name", "Price", "ServiceId" },
                values: new object[,]
                {
                    { 1, null, 30, "Haircut", 30.00m, "service-1" },
                    { 2, null, 120, "Hair Coloring", 80.00m, "service-2" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "Email", "Location", "Name", "StoreId" },
                values: new object[,]
                {
                    { 1, "hairsalon@example.com", "Los Angeles", "Hair Salon A", "store-1" },
                    { 2, "hairsalon@example.com", "Texas", "Hair Salon B", "store-2" },
                    { 3, "hairsalon@example.com", "New York", "Hair Salon C", "store-3" }
                });

            migrationBuilder.InsertData(
                table: "StoreHours",
                columns: new[] { "Id", "StoreId" },
                values: new object[,]
                {
                    { 1, "store-1" },
                    { 2, "store-2" },
                    { 3, "store-3" }
                });

            migrationBuilder.InsertData(
                table: "StoreServices",
                columns: new[] { "ServiceId", "StoreId" },
                values: new object[,]
                {
                    { "service-1", "store-1" },
                    { "service-2", "store-1" },
                    { "service-1", "store-2" },
                    { "service-2", "store-2" },
                    { "service-1", "store-3" },
                    { "service-2", "store-3" }
                });

            migrationBuilder.InsertData(
                table: "RegularHour",
                columns: new[] { "Id", "Close", "Day", "IsClosed", "Open", "StoreHourId" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 18, 0, 0, 0), 1, false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 2, new TimeSpan(0, 18, 0, 0, 0), 2, false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 3, new TimeSpan(0, 18, 0, 0, 0), 3, false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 4, new TimeSpan(0, 18, 0, 0, 0), 4, false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 5, new TimeSpan(0, 18, 0, 0, 0), 5, false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 6, new TimeSpan(0, 17, 0, 0, 0), 6, false, new TimeSpan(0, 10, 0, 0, 0), 1 },
                    { 7, new TimeSpan(0, 0, 0, 0, 0), 0, true, new TimeSpan(0, 0, 0, 0, 0), 1 },
                    { 8, new TimeSpan(0, 18, 0, 0, 0), 1, false, new TimeSpan(0, 9, 0, 0, 0), 2 },
                    { 9, new TimeSpan(0, 18, 0, 0, 0), 2, false, new TimeSpan(0, 9, 0, 0, 0), 2 },
                    { 10, new TimeSpan(0, 18, 0, 0, 0), 3, false, new TimeSpan(0, 9, 0, 0, 0), 2 },
                    { 11, new TimeSpan(0, 18, 0, 0, 0), 4, false, new TimeSpan(0, 9, 0, 0, 0), 2 },
                    { 12, new TimeSpan(0, 18, 0, 0, 0), 5, false, new TimeSpan(0, 9, 0, 0, 0), 2 },
                    { 13, new TimeSpan(0, 17, 0, 0, 0), 6, false, new TimeSpan(0, 10, 0, 0, 0), 2 },
                    { 14, new TimeSpan(0, 0, 0, 0, 0), 0, true, new TimeSpan(0, 0, 0, 0, 0), 2 },
                    { 15, new TimeSpan(0, 18, 0, 0, 0), 1, false, new TimeSpan(0, 9, 0, 0, 0), 3 },
                    { 16, new TimeSpan(0, 18, 0, 0, 0), 2, false, new TimeSpan(0, 9, 0, 0, 0), 3 },
                    { 17, new TimeSpan(0, 18, 0, 0, 0), 3, false, new TimeSpan(0, 9, 0, 0, 0), 3 },
                    { 18, new TimeSpan(0, 18, 0, 0, 0), 4, false, new TimeSpan(0, 9, 0, 0, 0), 3 },
                    { 19, new TimeSpan(0, 18, 0, 0, 0), 5, false, new TimeSpan(0, 9, 0, 0, 0), 3 },
                    { 20, new TimeSpan(0, 17, 0, 0, 0), 6, false, new TimeSpan(0, 10, 0, 0, 0), 3 },
                    { 21, new TimeSpan(0, 0, 0, 0, 0), 0, true, new TimeSpan(0, 0, 0, 0, 0), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegularHour_StoreHourId_Day",
                table: "RegularHour",
                columns: new[] { "StoreHourId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ServiceId",
                table: "Reservations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StoreId",
                table: "Reservations",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceId",
                table: "Services",
                column: "ServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialDate_StoreHourId_Date",
                table: "SpecialDate",
                columns: new[] { "StoreHourId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreHours_StoreId",
                table: "StoreHours",
                column: "StoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreId",
                table: "Stores",
                column: "StoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreServices_ServiceId",
                table: "StoreServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegularHour");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "SpecialDate");

            migrationBuilder.DropTable(
                name: "StoreServices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "StoreHours");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
