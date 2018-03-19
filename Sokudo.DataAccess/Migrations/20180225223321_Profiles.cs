using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sokudo.DataAccess.Migrations
{
    public partial class Profiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverProfileId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DriverProfile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverTransport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(nullable: true),
                    DriverProfileId = table.Column<int>(nullable: true),
                    TransportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTransport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverTransport_DriverProfile_DriverProfileId",
                        column: x => x.DriverProfileId,
                        principalTable: "DriverProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DriverTransport_TransportDefinitions_TransportId",
                        column: x => x.TransportId,
                        principalTable: "TransportDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DriverProfileId",
                table: "Users",
                column: "DriverProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverTransport_DriverProfileId",
                table: "DriverTransport",
                column: "DriverProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverTransport_TransportId",
                table: "DriverTransport",
                column: "TransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DriverProfile_DriverProfileId",
                table: "Users",
                column: "DriverProfileId",
                principalTable: "DriverProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_DriverProfile_DriverProfileId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "DriverTransport");

            migrationBuilder.DropTable(
                name: "DriverProfile");

            migrationBuilder.DropIndex(
                name: "IX_Users_DriverProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DriverProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");
        }
    }
}
