﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddRawCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO tbl_category VALUES('Cat 1')");
            migrationBuilder.Sql("INSERT INTO tbl_category VALUES('Cat 3')");
            migrationBuilder.Sql("INSERT INTO tbl_category VALUES('Cat 2')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
