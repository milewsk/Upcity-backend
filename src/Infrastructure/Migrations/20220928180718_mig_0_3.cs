using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Infrastructure.Migrations
{
    public partial class mig_0_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoyalityProgramAccount_Users_UserID",
                table: "LoyalityProgramAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceTags_Tag_TagID",
                table: "PlaceTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tag_TagID",
                table: "ProductTags");

            migrationBuilder.DropIndex(
                name: "IX_UserClaims_UserID",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoyalityProgramAccount",
                table: "LoyalityProgramAccount");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Coordinates");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "LoyalityProgramAccount",
                newName: "LoyalityProgramAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_LoyalityProgramAccount_UserID",
                table: "LoyalityProgramAccounts",
                newName: "IX_LoyalityProgramAccounts_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "UsersDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UsersDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ProductTags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationDate",
                table: "ProductTags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductCategoryID",
                table: "Products",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "PlaceTags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationDate",
                table: "PlaceTags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "PlacesDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PlacesDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIP",
                table: "PlacesDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PlacesDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "PlacesDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterFirm",
                table: "PlacesDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Coordinates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationDate",
                table: "Coordinates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Coordinates",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Tags",
                nullable: false,
                defaultValue: "#fff");

            migrationBuilder.AddColumn<byte>(
                name: "IsActive",
                table: "Tags",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "LoyalityProgramAccounts",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationDate",
                table: "LoyalityProgramAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "LoyalityProgramAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoyalityProgramAccounts",
                table: "LoyalityProgramAccounts",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "PlaceOpinion",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastModificationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    PlaceID = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceOpinion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlaceOpinion_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastModificationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserID",
                table: "UserClaims",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceOpinion_PlaceID",
                table: "PlaceOpinion",
                column: "PlaceID");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyalityProgramAccounts_Users_UserID",
                table: "LoyalityProgramAccounts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceTags_Tags_TagID",
                table: "PlaceTags",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategory_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID",
                principalTable: "ProductCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tags_TagID",
                table: "ProductTags",
                column: "TagID",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoyalityProgramAccounts_Users_UserID",
                table: "LoyalityProgramAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceTags_Tags_TagID",
                table: "PlaceTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategory_ProductCategoryID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagID",
                table: "ProductTags");

            migrationBuilder.DropTable(
                name: "PlaceOpinion");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_UserClaims_UserID",
                table: "UserClaims");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCategoryID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoyalityProgramAccounts",
                table: "LoyalityProgramAccounts");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ProductTags");

            migrationBuilder.DropColumn(
                name: "LastModificationDate",
                table: "ProductTags");

            migrationBuilder.DropColumn(
                name: "ProductCategoryID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "PlaceTags");

            migrationBuilder.DropColumn(
                name: "LastModificationDate",
                table: "PlaceTags");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "PlacesDetails");

            migrationBuilder.DropColumn(
                name: "City",
                table: "PlacesDetails");

            migrationBuilder.DropColumn(
                name: "NIP",
                table: "PlacesDetails");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PlacesDetails");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "PlacesDetails");

            migrationBuilder.DropColumn(
                name: "RegisterFirm",
                table: "PlacesDetails");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "LastModificationDate",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "LoyalityProgramAccounts",
                newName: "LoyalityProgramAccount");

            migrationBuilder.RenameIndex(
                name: "IX_LoyalityProgramAccounts_UserID",
                table: "LoyalityProgramAccount",
                newName: "IX_LoyalityProgramAccount_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "UsersDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UsersDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Coordinates",
                type: "decimal(10,7)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Coordinates",
                type: "decimal(10,7)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "LoyalityProgramAccount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModificationDate",
                table: "LoyalityProgramAccount",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "LoyalityProgramAccount",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoyalityProgramAccount",
                table: "LoyalityProgramAccount",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserID",
                table: "UserClaims",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_LoyalityProgramAccount_Users_UserID",
                table: "LoyalityProgramAccount",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceTags_Tag_TagID",
                table: "PlaceTags",
                column: "TagID",
                principalTable: "Tag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tag_TagID",
                table: "ProductTags",
                column: "TagID",
                principalTable: "Tag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
