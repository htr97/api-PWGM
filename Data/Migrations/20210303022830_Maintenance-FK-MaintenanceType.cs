using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Data.Migrations
{
    public partial class MaintenanceFKMaintenanceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_MaintenanceTypes_MaintenanceTypeId",
                table: "Maintenances");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceTypeId",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_MaintenanceTypes_MaintenanceTypeId",
                table: "Maintenances",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_MaintenanceTypes_MaintenanceTypeId",
                table: "Maintenances");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceTypeId",
                table: "Maintenances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_MaintenanceTypes_MaintenanceTypeId",
                table: "Maintenances",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
