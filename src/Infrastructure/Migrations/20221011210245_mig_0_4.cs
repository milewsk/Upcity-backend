using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class mig_0_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceOpinion_Places_PlaceID",
                table: "PlaceOpinion");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Places_PlaceID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategory_ProductCategoryID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_Products_PlaceID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCategoryID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceOpinion",
                table: "PlaceOpinion");

            migrationBuilder.DropColumn(
                name: "PlaceID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCategoryID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Places");

            migrationBuilder.RenameTable(
                name: "PlaceOpinion",
                newName: "PlaceOpinions");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceOpinion_PlaceID",
                table: "PlaceOpinions",
                newName: "IX_PlaceOpinions_PlaceID");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaceMenuCategoryID",
                table: "Products",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Places",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceOpinions",
                table: "PlaceOpinions",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "PlaceMenus",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastModificationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    PlaceID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceMenus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlaceMenus_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceOpeningHours",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastModificationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    DayOfWeek = table.Column<byte>(nullable: false),
                    Opens = table.Column<TimeSpan>(nullable: false),
                    Closes = table.Column<TimeSpan>(nullable: false),
                    PlaceID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceOpeningHours", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlaceOpeningHours_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceMenuCategories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastModificationDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Name = table.Column<string>(nullable: true),
                    PlaceMenuID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceMenuCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlaceMenuCategories_PlaceMenus_PlaceMenuID",
                        column: x => x.PlaceMenuID,
                        principalTable: "PlaceMenus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PlaceMenuCategoryID",
                table: "Products",
                column: "PlaceMenuCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceMenuCategories_PlaceMenuID",
                table: "PlaceMenuCategories",
                column: "PlaceMenuID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceMenus_PlaceID",
                table: "PlaceMenus",
                column: "PlaceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaceOpeningHours_PlaceID",
                table: "PlaceOpeningHours",
                column: "PlaceID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceOpinions_Places_PlaceID",
                table: "PlaceOpinions",
                column: "PlaceID",
                principalTable: "Places",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PlaceMenuCategories_PlaceMenuCategoryID",
                table: "Products",
                column: "PlaceMenuCategoryID",
                principalTable: "PlaceMenuCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceOpinions_Places_PlaceID",
                table: "PlaceOpinions");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_PlaceMenuCategories_PlaceMenuCategoryID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "PlaceMenuCategories");

            migrationBuilder.DropTable(
                name: "PlaceOpeningHours");

            migrationBuilder.DropTable(
                name: "PlaceMenus");

            migrationBuilder.DropIndex(
                name: "IX_Products_PlaceMenuCategoryID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceOpinions",
                table: "PlaceOpinions");

            migrationBuilder.DropColumn(
                name: "PlaceMenuCategoryID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Places");

            migrationBuilder.RenameTable(
                name: "PlaceOpinions",
                newName: "PlaceOpinion");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceOpinions_PlaceID",
                table: "PlaceOpinion",
                newName: "IX_PlaceOpinion_PlaceID");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaceID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductCategoryID",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceOpinion",
                table: "PlaceOpinion",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PlaceID",
                table: "Products",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceOpinion_Places_PlaceID",
                table: "PlaceOpinion",
                column: "PlaceID",
                principalTable: "Places",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Places_PlaceID",
                table: "Products",
                column: "PlaceID",
                principalTable: "Places",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategory_ProductCategoryID",
                table: "Products",
                column: "ProductCategoryID",
                principalTable: "ProductCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
