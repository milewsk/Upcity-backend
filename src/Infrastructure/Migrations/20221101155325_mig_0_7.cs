using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Infrastructure.Migrations
{
    public partial class mig_0_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceOpeningHours_Places_PlaceID",
                table: "PlaceOpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_PlaceOpeningHours_PlaceID",
                table: "PlaceOpeningHours");

            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "Coordinates",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geography");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceOpeningHours_PlaceID",
                table: "PlaceOpeningHours",
                column: "PlaceID");

            migrationBuilder.AddForeignKey(
                name: "FK_OpeningHours_Places_PlaceID",
                table: "PlaceOpeningHours",
                column: "PlaceID",
                principalTable: "Places",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpeningHours_Places_PlaceID",
                table: "PlaceOpeningHours");

            migrationBuilder.DropIndex(
                name: "IX_PlaceOpeningHours_PlaceID",
                table: "PlaceOpeningHours");

            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "Coordinates",
                type: "geography",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceOpeningHours_PlaceID",
                table: "PlaceOpeningHours",
                column: "PlaceID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceOpeningHours_Places_PlaceID",
                table: "PlaceOpeningHours",
                column: "PlaceID",
                principalTable: "Places",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
