using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationShips.Migrations
{
    public partial class deleteModes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeature_Products_ProductId",
                table: "ProductFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentId",
                table: "StudentTeacherWithManyToMany");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherId",
                table: "StudentTeacherWithManyToMany");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeature_ProductId",
                table: "ProductFeature");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductFeature",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_ProductId",
                table: "ProductFeature",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeature_Products_ProductId",
                table: "ProductFeature",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentId",
                table: "StudentTeacherWithManyToMany",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherId",
                table: "StudentTeacherWithManyToMany",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeature_Products_ProductId",
                table: "ProductFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentId",
                table: "StudentTeacherWithManyToMany");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherId",
                table: "StudentTeacherWithManyToMany");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeature_ProductId",
                table: "ProductFeature");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductFeature",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeature_ProductId",
                table: "ProductFeature",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeature_Products_ProductId",
                table: "ProductFeature",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentId",
                table: "StudentTeacherWithManyToMany",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherId",
                table: "StudentTeacherWithManyToMany",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
