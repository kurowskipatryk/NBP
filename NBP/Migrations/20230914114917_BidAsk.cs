using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBP.Migrations
{
    public partial class BidAsk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Ask",
                table: "Rates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Bid",
                table: "Rates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ask",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Bid",
                table: "Rates");
        }
    }
}
