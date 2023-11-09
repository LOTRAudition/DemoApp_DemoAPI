using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApp_DemoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToDemoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DemoID",
                table: "DemoNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Demos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 18, 33, 293, DateTimeKind.Local).AddTicks(9630));

            migrationBuilder.UpdateData(
                table: "Demos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 18, 33, 293, DateTimeKind.Local).AddTicks(9681));

            migrationBuilder.UpdateData(
                table: "Demos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 18, 33, 293, DateTimeKind.Local).AddTicks(9684));

            migrationBuilder.UpdateData(
                table: "Demos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 18, 33, 293, DateTimeKind.Local).AddTicks(9686));

            migrationBuilder.UpdateData(
                table: "Demos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 18, 33, 293, DateTimeKind.Local).AddTicks(9689));

            migrationBuilder.CreateIndex(
                name: "IX_DemoNumbers_DemoID",
                table: "DemoNumbers",
                column: "DemoID");

            migrationBuilder.AddForeignKey(
                name: "FK_DemoNumbers_Demos_DemoID",
                table: "DemoNumbers",
                column: "DemoID",
                principalTable: "Demos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DemoNumbers_Demos_DemoID",
                table: "DemoNumbers");

            migrationBuilder.DropIndex(
                name: "IX_DemoNumbers_DemoID",
                table: "DemoNumbers");

            migrationBuilder.DropColumn(
                name: "DemoID",
                table: "DemoNumbers");

            migrationBuilder.UpdateData(
                table: "Demos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 0, 54, 527, DateTimeKind.Local).AddTicks(8155));

            migrationBuilder.UpdateData(
                table: "Demos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 0, 54, 527, DateTimeKind.Local).AddTicks(8267));

            migrationBuilder.UpdateData(
                table: "Demos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 0, 54, 527, DateTimeKind.Local).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Demos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 0, 54, 527, DateTimeKind.Local).AddTicks(8272));

            migrationBuilder.UpdateData(
                table: "Demos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 7, 4, 10, 0, 54, 527, DateTimeKind.Local).AddTicks(8274));
        }
    }
}
