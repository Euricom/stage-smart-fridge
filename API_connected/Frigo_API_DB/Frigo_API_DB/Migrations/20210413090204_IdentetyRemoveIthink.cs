using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Frigo_API_DB.Migrations
{
    public partial class IdentetyRemoveIthink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Persons",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Persons");
        }
    }
}
