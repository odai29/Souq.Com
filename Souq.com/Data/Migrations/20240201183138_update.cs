using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Souq.com.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "orders");

            //migrationBuilder.AddColumn<string>(
            //    name: "Name",
            //    table: "orders",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "orders",
                type: "string",
                nullable: true);
        }
    }
}
