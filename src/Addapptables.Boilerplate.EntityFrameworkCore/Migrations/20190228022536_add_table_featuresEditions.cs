using Microsoft.EntityFrameworkCore.Migrations;

namespace Addapptables.Boilerplate.Migrations
{
    public partial class add_table_featuresEditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AbpEditions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EditionType",
                table: "AbpEditions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "AbpEditions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfUsers",
                table: "AbpEditions",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "AbpEditions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrialDayCount",
                table: "AbpEditions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AbpEditions");

            migrationBuilder.DropColumn(
                name: "EditionType",
                table: "AbpEditions");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "AbpEditions");

            migrationBuilder.DropColumn(
                name: "NumberOfUsers",
                table: "AbpEditions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "AbpEditions");

            migrationBuilder.DropColumn(
                name: "TrialDayCount",
                table: "AbpEditions");
        }
    }
}
