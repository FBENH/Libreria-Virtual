﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreriaVirtualData.Library.Migrations
{
    /// <inheritdoc />
    public partial class OptimisticLocking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Libro",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Libro");
        }
    }
}
