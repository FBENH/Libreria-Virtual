using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreriaVirtualData.Library.Migrations
{
    /// <inheritdoc />
    public partial class LibroCalificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Calificacion",
                table: "Libro",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calificacion",
                table: "Libro");
        }
    }
}
