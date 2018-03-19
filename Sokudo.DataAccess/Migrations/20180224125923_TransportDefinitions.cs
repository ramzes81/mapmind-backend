using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sokudo.DataAccess.Migrations
{
    public partial class TransportDefinitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportManufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportManufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ManufacturerId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportModel_TransportManufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "TransportManufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ManufacturerId = table.Column<int>(nullable: true),
                    ModelId = table.Column<int>(nullable: true),
                    SeatsCount = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportDefinitions_TransportManufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "TransportManufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportDefinitions_TransportModel_ModelId",
                        column: x => x.ModelId,
                        principalTable: "TransportModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportDefinitions_ManufacturerId",
                table: "TransportDefinitions",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportDefinitions_ModelId",
                table: "TransportDefinitions",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportModel_ManufacturerId",
                table: "TransportModel",
                column: "ManufacturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportDefinitions");

            migrationBuilder.DropTable(
                name: "TransportModel");

            migrationBuilder.DropTable(
                name: "TransportManufacturer");
        }
    }
}
