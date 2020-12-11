namespace ProjectRestaurant.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddTableNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableNumber",
                table: "Tables",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableNumber",
                table: "Tables");
        }
    }
}
