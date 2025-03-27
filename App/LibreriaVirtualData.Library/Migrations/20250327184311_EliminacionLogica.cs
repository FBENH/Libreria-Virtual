using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreriaVirtualData.Library.Migrations
{
    /// <inheritdoc />
    public partial class EliminacionLogica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Usuario");
        }
    }
}
