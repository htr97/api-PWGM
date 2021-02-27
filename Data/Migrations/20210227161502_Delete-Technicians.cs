using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Data.Migrations
{
    public partial class DeleteTechnicians : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Technicians_TechnicianId",
                table: "Maintenances");

            migrationBuilder.DropTable(
                name: "Technicians");

            migrationBuilder.RenameColumn(
                name: "TechnicianId",
                table: "Maintenances",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenances_TechnicianId",
                table: "Maintenances",
                newName: "IX_Maintenances_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Users_UserId",
                table: "Maintenances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Users_UserId",
                table: "Maintenances");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Maintenances",
                newName: "TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_Maintenances_UserId",
                table: "Maintenances",
                newName: "IX_Maintenances_TechnicianId");

            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technicians_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_CompanyId",
                table: "Technicians",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Technicians_TechnicianId",
                table: "Maintenances",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
