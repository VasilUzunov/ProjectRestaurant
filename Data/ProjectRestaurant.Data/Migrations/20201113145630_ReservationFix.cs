namespace ProjectRestaurant.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ReservationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_TableId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TableId1",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TableId1",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "TableId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TableId1",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId1",
                table: "Reservations",
                column: "TableId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_TableId1",
                table: "Reservations",
                column: "TableId1",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
