namespace ProjectRestaurant.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DeleteSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "FavoriteMenuItems");

            migrationBuilder.DropTable(
                name: "ReservationMenus");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Reservations",
                maxLength: 200,
                nullable: false,
                defaultValue: " ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: true),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationMenus_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationMenus_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteMenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavoriteId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteMenuItems_Favorites_FavoriteId",
                        column: x => x.FavoriteId,
                        principalTable: "Favorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteMenuItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMenuItems_FavoriteId",
                table: "FavoriteMenuItems",
                column: "FavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMenuItems_MenuItemId",
                table: "FavoriteMenuItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_IsDeleted",
                table: "Favorites",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationMenus_MenuItemId",
                table: "ReservationMenus",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationMenus_ReservationId",
                table: "ReservationMenus",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
