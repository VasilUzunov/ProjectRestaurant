namespace ProjectRestaurant.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MenuImageAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "MenuItems");

            migrationBuilder.CreateTable(
                name: "MenuImages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    MenuItemId = table.Column<int>(nullable: false),
                    Extension = table.Column<string>(nullable: true),
                    RemoteImageUrl = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuImages_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuImages_MenuItemId",
                table: "MenuImages",
                column: "MenuItemId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuImages");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
