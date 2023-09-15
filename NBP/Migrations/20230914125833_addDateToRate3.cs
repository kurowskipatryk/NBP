using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBP.Migrations
{
    public partial class addDateToRate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "Rates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "Rates",
                type: "datetime2",
                nullable: true);
        }
    }
}
