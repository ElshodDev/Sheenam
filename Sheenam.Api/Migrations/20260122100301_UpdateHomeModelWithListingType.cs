using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Sheenam.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHomeModelWithListingType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrls",
                table: "Homes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Homes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ListedDate",
                table: "Homes",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "ListingType",
                table: "Homes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyRent",
                table: "Homes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Homes",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SecurityDeposit",
                table: "Homes",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "ListedDate",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "ListingType",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "MonthlyRent",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "SecurityDeposit",
                table: "Homes");
        }
    }
}
