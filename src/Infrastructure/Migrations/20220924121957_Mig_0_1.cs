using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Mig_0_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UsersDetails");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UsersDetails",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Places",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "IsActive",
                table: "Places",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastModificationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    UserID = table.Column<Guid>(nullable: false),
                    Value = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserID",
                table: "UserClaims",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UsersDetails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Places");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UsersDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
