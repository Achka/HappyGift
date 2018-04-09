using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HappyGift.Migrations
{
    public partial class ForeignKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartServices_Services_ServiceId1",
                table: "CartServices");

            migrationBuilder.DropIndex(
                name: "IX_CartServices_ServiceId1",
                table: "CartServices");

            migrationBuilder.DropColumn(
                name: "ServiceId1",
                table: "CartServices");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "CartServices",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_CartServices_ServiceId",
                table: "CartServices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartServices_Services_ServiceId",
                table: "CartServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartServices_Services_ServiceId",
                table: "CartServices");

            migrationBuilder.DropIndex(
                name: "IX_CartServices_ServiceId",
                table: "CartServices");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "CartServices",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ServiceId1",
                table: "CartServices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartServices_ServiceId1",
                table: "CartServices",
                column: "ServiceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartServices_Services_ServiceId1",
                table: "CartServices",
                column: "ServiceId1",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
