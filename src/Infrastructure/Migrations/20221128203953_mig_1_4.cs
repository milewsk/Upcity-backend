using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class mig_1_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Places_PlaceID",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_PlaceID",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "PlaceID",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "PrivateMessages");

            migrationBuilder.RenameIndex(
                name: "IX_Message_UserID",
                table: "PrivateMessages",
                newName: "IX_PrivateMessages_UserID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "PrivateMessages",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateMessages",
                table: "PrivateMessages",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "PlaceMessages",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastModificationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Date = table.Column<string>(nullable: true),
                    PlaceID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Message_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceMessages_PlaceID",
                table: "PlaceMessages",
                column: "PlaceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateMessages",
                table: "PrivateMessages");

            migrationBuilder.RenameTable(
                name: "PrivateMessages",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_PrivateMessages_UserID",
                table: "Message",
                newName: "IX_Message_UserID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "Message",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Message",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaceID",
                table: "Message",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_PlaceID",
                table: "Message",
                column: "PlaceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Places_PlaceID",
                table: "Message",
                column: "PlaceID",
                principalTable: "Places",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
