using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KütüphaneOtomasyonu.Migrations
{
    /// <inheritdoc />
    public partial class ucuncu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedbookTitle",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberId",
                table: "Members",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_MemberId",
                table: "Books",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_MemberId",
                table: "Books",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Members_MemberId",
                table: "Members",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_MemberId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Members_MemberId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_MemberId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Books_MemberId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "ReceivedbookTitle",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
