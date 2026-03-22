using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionMonitoring.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRiskScoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RiskLevel",
                table: "Alerts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "RiskScore",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RiskLevel",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "RiskScore",
                table: "Alerts");
        }
    }
}
