using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrosWebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterGenero_Add_Coluna_Ativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "Ativo",
                table: "Genero",
                type: "bit(1)",
                nullable: false,
                defaultValue: 0ul);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Genero");
        }
    }
}
