using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PecasAntunes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCodigoInternoAutoPeca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoInterno",
                table: "AutoPecas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoInterno",
                table: "AutoPecas");
        }
    }
}
