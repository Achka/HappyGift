using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HappyGift.Migrations
{
    public partial class ChangeKeyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftServices_Gifts_GiftId1",
                table: "GiftServices");

            migrationBuilder.DropForeignKey(
                name: "FK_GiftServices_Services_ServiceId1",
                table: "GiftServices");

            migrationBuilder.DropIndex(
                name: "IX_GiftServices_GiftId1",
                table: "GiftServices");

            migrationBuilder.DropIndex(
                name: "IX_GiftServices_ServiceId1",
                table: "GiftServices");

            migrationBuilder.DropColumn(
                name: "GiftId1",
                table: "GiftServices");

            migrationBuilder.DropColumn(
                name: "ServiceId1",
                table: "GiftServices");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "GiftServices",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "GiftId",
                table: "GiftServices",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_GiftServices_GiftId",
                table: "GiftServices",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftServices_ServiceId",
                table: "GiftServices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftServices_Gifts_GiftId",
                table: "GiftServices",
                column: "GiftId",
                principalTable: "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftServices_Services_ServiceId",
                table: "GiftServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiftServices_Gifts_GiftId",
                table: "GiftServices");

            migrationBuilder.DropForeignKey(
                name: "FK_GiftServices_Services_ServiceId",
                table: "GiftServices");

            migrationBuilder.DropIndex(
                name: "IX_GiftServices_GiftId",
                table: "GiftServices");

            migrationBuilder.DropIndex(
                name: "IX_GiftServices_ServiceId",
                table: "GiftServices");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "GiftServices",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "GiftId",
                table: "GiftServices",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "GiftId1",
                table: "GiftServices",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServiceId1",
                table: "GiftServices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiftServices_GiftId1",
                table: "GiftServices",
                column: "GiftId1");

            migrationBuilder.CreateIndex(
                name: "IX_GiftServices_ServiceId1",
                table: "GiftServices",
                column: "ServiceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GiftServices_Gifts_GiftId1",
                table: "GiftServices",
                column: "GiftId1",
                principalTable: "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GiftServices_Services_ServiceId1",
                table: "GiftServices",
                column: "ServiceId1",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
