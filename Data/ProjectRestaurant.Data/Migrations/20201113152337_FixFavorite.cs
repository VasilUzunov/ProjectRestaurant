namespace ProjectRestaurant.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_AspNetUsers_UserId",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMenuItems_Favorite_FavoriteId",
                table: "FavoriteMenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite");

            migrationBuilder.RenameTable(
                name: "Favorite",
                newName: "Favorites");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_UserId",
                table: "Favorites",
                newName: "IX_Favorites_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_IsDeleted",
                table: "Favorites",
                newName: "IX_Favorites_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMenuItems_Favorites_FavoriteId",
                table: "FavoriteMenuItems",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMenuItems_Favorites_FavoriteId",
                table: "FavoriteMenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "Favorite");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_UserId",
                table: "Favorite",
                newName: "IX_Favorite_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_IsDeleted",
                table: "Favorite",
                newName: "IX_Favorite_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_AspNetUsers_UserId",
                table: "Favorite",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMenuItems_Favorite_FavoriteId",
                table: "FavoriteMenuItems",
                column: "FavoriteId",
                principalTable: "Favorite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
