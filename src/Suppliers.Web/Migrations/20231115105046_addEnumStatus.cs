﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Suppliers.Web.Migrations
{
    /// <inheritdoc />
    public partial class addEnumStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Supplies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Supplies");
        }
    }
}
