using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectRestaurant.Data.Migrations
{
    public partial class AddShapeOfTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShapeOfTable",
                table: "Tables",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShapeOfTable",
                table: "Tables");
        }
    }
}
