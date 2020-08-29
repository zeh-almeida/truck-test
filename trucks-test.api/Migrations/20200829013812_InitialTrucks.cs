using Microsoft.EntityFrameworkCore.Migrations;
using System;

#pragma warning disable CA1062 // Validate arguments of public methods
#pragma warning disable IDE0058 // Expression value is never used
namespace TrucksTest.API.Migrations
{
    public partial class InitialTrucks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TruckTest");

            migrationBuilder.CreateTable(
                name: "Truck",
                schema: "TruckTest",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 36, nullable: false),
                    TruckType = table.Column<int>(nullable: false),
                    FabricationYear = table.Column<int>(nullable: false),
                    ModelYear = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Plate = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Truck",
                schema: "TruckTest");
        }
    }
}
#pragma warning restore CA1062 // Validate arguments of public methods
#pragma warning restore IDE0058 // Expression value is never used
