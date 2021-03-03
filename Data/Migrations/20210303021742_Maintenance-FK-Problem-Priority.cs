using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Data.Migrations
{
    public partial class MaintenanceFKProblemPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Priorities_PriorityId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Problems_ProblemId",
                table: "Maintenances");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemId",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PriorityId",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Priorities_PriorityId",
                table: "Maintenances",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Problems_ProblemId",
                table: "Maintenances",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Priorities_PriorityId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Problems_ProblemId",
                table: "Maintenances");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemId",
                table: "Maintenances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PriorityId",
                table: "Maintenances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Priorities_PriorityId",
                table: "Maintenances",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Problems_ProblemId",
                table: "Maintenances",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
