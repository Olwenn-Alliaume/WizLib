using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddForeignKeyAnnotationToBookAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Authors_Author_Id1",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Books_Book_Id1",
                table: "BookAuthor");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthor_Author_Id1",
                table: "BookAuthor");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthor_Book_Id1",
                table: "BookAuthor");

            migrationBuilder.DropColumn(
                name: "Author_Id1",
                table: "BookAuthor");

            migrationBuilder.DropColumn(
                name: "Book_Id1",
                table: "BookAuthor");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_Book_Id",
                table: "BookAuthor",
                column: "Book_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Authors_Author_Id",
                table: "BookAuthor",
                column: "Author_Id",
                principalTable: "Authors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Books_Book_Id",
                table: "BookAuthor",
                column: "Book_Id",
                principalTable: "Books",
                principalColumn: "Book_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Authors_Author_Id",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Books_Book_Id",
                table: "BookAuthor");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthor_Book_Id",
                table: "BookAuthor");

            migrationBuilder.AddColumn<int>(
                name: "Author_Id1",
                table: "BookAuthor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Book_Id1",
                table: "BookAuthor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_Author_Id1",
                table: "BookAuthor",
                column: "Author_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_Book_Id1",
                table: "BookAuthor",
                column: "Book_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Authors_Author_Id1",
                table: "BookAuthor",
                column: "Author_Id1",
                principalTable: "Authors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Books_Book_Id1",
                table: "BookAuthor",
                column: "Book_Id1",
                principalTable: "Books",
                principalColumn: "Book_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
