using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Heavy.Web.Data.Migrations
{
    public partial class roledata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "RoleEditViewModelId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "RoleCreateViewModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCreateViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleEditViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEditViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleEditViewModelId",
                table: "AspNetUsers",
                column: "RoleEditViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RoleEditViewModel_RoleEditViewModelId",
                table: "AspNetUsers",
                column: "RoleEditViewModelId",
                principalTable: "RoleEditViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RoleEditViewModel_RoleEditViewModelId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RoleCreateViewModel");

            migrationBuilder.DropTable(
                name: "RoleEditViewModel");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleEditViewModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleEditViewModelId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
