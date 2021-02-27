using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Data.Migrations
{
    public partial class Equipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Technician_TechnicianId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Ubications_UbicationId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Technician_Companies_CompanyId",
                table: "Technician");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_UbicationId",
                table: "Maintenances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Technician",
                table: "Technician");

            migrationBuilder.DropColumn(
                name: "UbicationId",
                table: "Maintenances");

            migrationBuilder.RenameTable(
                name: "Technician",
                newName: "Technicians");

            migrationBuilder.RenameIndex(
                name: "IX_Technician_CompanyId",
                table: "Technicians",
                newName: "IX_Technicians_CompanyId");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Technicians",
                table: "Technicians",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SystemType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    StorageType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Processor = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Memory = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    OsName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    UbicationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Ubications_UbicationId",
                        column: x => x.UbicationId,
                        principalTable: "Ubications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_EquipmentId",
                table: "Maintenances",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_UbicationId",
                table: "Equipments",
                column: "UbicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Equipments_EquipmentId",
                table: "Maintenances",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Technicians_TechnicianId",
                table: "Maintenances",
                column: "TechnicianId",
                principalTable: "Technicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Technicians_Companies_CompanyId",
                table: "Technicians",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Equipments_EquipmentId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Technicians_TechnicianId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicians_Companies_CompanyId",
                table: "Technicians");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_EquipmentId",
                table: "Maintenances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Technicians",
                table: "Technicians");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Maintenances");

            migrationBuilder.RenameTable(
                name: "Technicians",
                newName: "Technician");

            migrationBuilder.RenameIndex(
                name: "IX_Technicians_CompanyId",
                table: "Technician",
                newName: "IX_Technician_CompanyId");

            migrationBuilder.AddColumn<int>(
                name: "UbicationId",
                table: "Maintenances",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Technician",
                table: "Technician",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_UbicationId",
                table: "Maintenances",
                column: "UbicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Technician_TechnicianId",
                table: "Maintenances",
                column: "TechnicianId",
                principalTable: "Technician",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Ubications_UbicationId",
                table: "Maintenances",
                column: "UbicationId",
                principalTable: "Ubications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Technician_Companies_CompanyId",
                table: "Technician",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
