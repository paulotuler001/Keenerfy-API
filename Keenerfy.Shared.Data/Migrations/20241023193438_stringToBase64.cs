using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Keenerfy.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class stringToBase64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(
            @"UPDATE Products
              SET Link = CONVERT(varbinary(max), Link)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Link",
                table: "Products",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(
            @"UPDATE Products
              SET Link = CONVERT(nvarchar(max), Link)");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
