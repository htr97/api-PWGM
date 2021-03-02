using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Data.Migrations
{
    public partial class EquipmentFKMCap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Ubications_UbicationId",
                table: "Equipments");

            migrationBuilder.AlterColumn<int>(
                name: "UbicationId",
                table: "Equipments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageCap",
                table: "Equipments",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Ubications_UbicationId",
                table: "Equipments",
                column: "UbicationId",
                principalTable: "Ubications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Ubications_UbicationId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "StorageCap",
                table: "Equipments");

            migrationBuilder.AlterColumn<int>(
                name: "UbicationId",
                table: "Equipments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Ubications_UbicationId",
                table: "Equipments",
                column: "UbicationId",
                principalTable: "Ubications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
