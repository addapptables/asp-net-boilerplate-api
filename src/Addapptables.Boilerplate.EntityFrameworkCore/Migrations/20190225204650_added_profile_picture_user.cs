using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Addapptables.Boilerplate.Migrations
{
    public partial class added_profile_picture_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureBase64",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppBinaryObjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Bytes = table.Column<byte[]>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    GenericId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBinaryObjects", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_ProfilePictureId",
                table: "AbpUsers",
                column: "ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppBinaryObjects_ProfilePictureId",
                table: "AbpUsers",
                column: "ProfilePictureId",
                principalTable: "AppBinaryObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppBinaryObjects_ProfilePictureId",
                table: "AbpUsers");

            migrationBuilder.DropTable(
                name: "AppBinaryObjects");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_ProfilePictureId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureBase64",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AbpUsers");
        }
    }
}
