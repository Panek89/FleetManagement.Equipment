using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetManagement.Equipment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialEntitiesCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MANUFACTURERS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COUNTRY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MANUFACTURERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CARS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MANUFACTURER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MILEAGE = table.Column<decimal>(type: "decimal(2,0)", precision: 2, nullable: false),
                    PRODUCTION_YEAR = table.Column<int>(type: "int", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    INITIAL_VALUE_AMOUNT = table.Column<decimal>(type: "decimal(2,0)", precision: 2, nullable: false),
                    INITIAL_VALUE_CURRENCY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CURRENT_VALUE_AMOUNT = table.Column<decimal>(type: "decimal(2,0)", precision: 2, nullable: false),
                    CURRENT_VALUE_CURRENCY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TITLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CARS_MANUFACTURERS_MANUFACTURER_ID",
                        column: x => x.MANUFACTURER_ID,
                        principalTable: "MANUFACTURERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARS_MANUFACTURER_ID",
                table: "CARS",
                column: "MANUFACTURER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CARS");

            migrationBuilder.DropTable(
                name: "MANUFACTURERS");
        }
    }
}
