namespace ProjectRestaurant.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventImage_Events_EventId",
                table: "EventImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventImage",
                table: "EventImage");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "EventImage");

            migrationBuilder.RenameTable(
                name: "EventImage",
                newName: "EventImages");

            migrationBuilder.RenameIndex(
                name: "IX_EventImage_EventId",
                table: "EventImages",
                newName: "IX_EventImages_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventImages",
                table: "EventImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventImages_Events_EventId",
                table: "EventImages",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventImages_Events_EventId",
                table: "EventImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventImages",
                table: "EventImages");

            migrationBuilder.RenameTable(
                name: "EventImages",
                newName: "EventImage");

            migrationBuilder.RenameIndex(
                name: "IX_EventImages_EventId",
                table: "EventImage",
                newName: "IX_EventImage_EventId");

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "EventImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventImage",
                table: "EventImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventImage_Events_EventId",
                table: "EventImage",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
