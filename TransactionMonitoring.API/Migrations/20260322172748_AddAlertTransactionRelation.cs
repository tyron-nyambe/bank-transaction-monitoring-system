using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionMonitoring.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAlertTransactionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Alerts_TransactionId",
                table: "Alerts",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Transactions_TransactionId",
                table: "Alerts",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Transactions_TransactionId",
                table: "Alerts");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_TransactionId",
                table: "Alerts");
        }
    }
}
