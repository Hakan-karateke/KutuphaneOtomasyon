using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KütüphaneOtomasyonu.Migrations
{
    /// <inheritdoc />
    public partial class sadf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdminRol",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminRol",
                table: "Members");
        }
    }
}
