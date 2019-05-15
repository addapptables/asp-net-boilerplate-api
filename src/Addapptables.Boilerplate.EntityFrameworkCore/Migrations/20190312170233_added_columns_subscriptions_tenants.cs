using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Addapptables.Boilerplate.Migrations
{
    public partial class added_columns_subscriptions_tenants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInTrialPeriod",
                table: "AbpTenants",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscriptionExpired",
                table: "AbpTenants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "NextPrice",
                table: "AbpTenants",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEndDate",
                table: "AbpTenants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInTrialPeriod",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "IsSubscriptionExpired",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "NextPrice",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "SubscriptionEndDate",
                table: "AbpTenants");
        }
    }
}
