﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentitySeverDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddJWTVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "JWTVersion",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JWTVersion",
                table: "AspNetUsers");
        }
    }
}
