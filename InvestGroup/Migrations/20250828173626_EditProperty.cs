using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestGroup.Migrations
{
    /// <inheritdoc />
    public partial class EditProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryInfoAr",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryInfoEn",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeaturesAr",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeaturesEn",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaintenanceInfoAr",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaintenanceInfoEn",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OverviewAr",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OverviewEn",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectAreaAr",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectAreaEn",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpecialOfferAr",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SpecialOfferEn",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnitTypesAr",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnitTypesEn",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryInfoAr",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DeliveryInfoEn",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "FeaturesAr",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "FeaturesEn",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "MaintenanceInfoAr",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "MaintenanceInfoEn",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OverviewAr",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OverviewEn",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ProjectAreaAr",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ProjectAreaEn",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "SpecialOfferAr",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "SpecialOfferEn",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UnitTypesAr",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UnitTypesEn",
                table: "Properties");
        }
    }
}
