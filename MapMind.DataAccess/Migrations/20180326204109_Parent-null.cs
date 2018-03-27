using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MapMind.DataAccess.Migrations
{
    public partial class Parentnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapNode_MindMap_MapId",
                table: "MapNode");

            migrationBuilder.DropForeignKey(
                name: "FK_MapNode_MapNode_ParentId",
                table: "MapNode");

            migrationBuilder.DropForeignKey(
                name: "FK_MindMap_Users_UserId",
                table: "MindMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MindMap",
                table: "MindMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapNode",
                table: "MapNode");

            migrationBuilder.RenameTable(
                name: "MindMap",
                newName: "Maps");

            migrationBuilder.RenameTable(
                name: "MapNode",
                newName: "MapNodes");

            migrationBuilder.RenameIndex(
                name: "IX_MindMap_UserId",
                table: "Maps",
                newName: "IX_Maps_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MapNode_ParentId",
                table: "MapNodes",
                newName: "IX_MapNodes_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_MapNode_MapId",
                table: "MapNodes",
                newName: "IX_MapNodes_MapId");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "MapNodes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maps",
                table: "Maps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapNodes",
                table: "MapNodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MapNodes_Maps_MapId",
                table: "MapNodes",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MapNodes_MapNodes_ParentId",
                table: "MapNodes",
                column: "ParentId",
                principalTable: "MapNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maps_Users_UserId",
                table: "Maps",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapNodes_Maps_MapId",
                table: "MapNodes");

            migrationBuilder.DropForeignKey(
                name: "FK_MapNodes_MapNodes_ParentId",
                table: "MapNodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Maps_Users_UserId",
                table: "Maps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maps",
                table: "Maps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapNodes",
                table: "MapNodes");

            migrationBuilder.RenameTable(
                name: "Maps",
                newName: "MindMap");

            migrationBuilder.RenameTable(
                name: "MapNodes",
                newName: "MapNode");

            migrationBuilder.RenameIndex(
                name: "IX_Maps_UserId",
                table: "MindMap",
                newName: "IX_MindMap_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MapNodes_ParentId",
                table: "MapNode",
                newName: "IX_MapNode_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_MapNodes_MapId",
                table: "MapNode",
                newName: "IX_MapNode_MapId");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "MapNode",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MindMap",
                table: "MindMap",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapNode",
                table: "MapNode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MapNode_MindMap_MapId",
                table: "MapNode",
                column: "MapId",
                principalTable: "MindMap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MapNode_MapNode_ParentId",
                table: "MapNode",
                column: "ParentId",
                principalTable: "MapNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MindMap_Users_UserId",
                table: "MindMap",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
