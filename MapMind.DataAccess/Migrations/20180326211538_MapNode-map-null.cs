using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MapMind.DataAccess.Migrations
{
    public partial class MapNodemapnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MapNodes_MapId",
                table: "MapNodes");

            migrationBuilder.AlterColumn<int>(
                name: "MapId",
                table: "MapNodes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_MapNodes_MapId",
                table: "MapNodes",
                column: "MapId",
                unique: true,
                filter: "[MapId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MapNodes_MapId",
                table: "MapNodes");

            migrationBuilder.AlterColumn<int>(
                name: "MapId",
                table: "MapNodes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MapNodes_MapId",
                table: "MapNodes",
                column: "MapId",
                unique: true);
        }
    }
}
