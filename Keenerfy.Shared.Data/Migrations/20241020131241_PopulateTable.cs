using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Keenerfy.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class PopulateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Products", new string[] { "Code", "Name", "Description", "Price", "Link", "Stock_id" }, new object[] { "kl001", "keeener_30", "The best keeeeeeeeener of the universe", 980.40f, null, 2 });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
