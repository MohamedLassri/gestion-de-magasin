using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestion_de_magasin.Migrations
{
    /// <inheritdoc />
    public partial class AdddescriptionArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Articles");
        }
    }
}
