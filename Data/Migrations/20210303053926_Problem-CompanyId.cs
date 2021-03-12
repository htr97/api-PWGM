using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Data.Migrations
{
    public partial class ProblemCompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Problems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Problems_CompanyId",
                table: "Problems",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_Companies_CompanyId",
                table: "Problems",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_Companies_CompanyId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_CompanyId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Problems");
        }
    }
}
