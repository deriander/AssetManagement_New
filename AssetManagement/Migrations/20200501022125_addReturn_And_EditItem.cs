using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.Migrations
{
    public partial class addReturn_And_EditItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specification",
                table: "TB_M_Item",
                newName: "Storage");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TB_M_Item",
                newName: "Ram");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "TB_M_Item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpu",
                table: "TB_M_Item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Display",
                table: "TB_M_Item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gpu",
                table: "TB_M_Item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Os",
                table: "TB_M_Item",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_T_Return",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Return_Date = table.Column<DateTimeOffset>(nullable: false),
                    Condition = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    User_Id = table.Column<int>(nullable: false),
                    Item_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_Return", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_T_Return_TB_M_Item_Item_Id",
                        column: x => x.Item_Id,
                        principalTable: "TB_M_Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_T_Return_TB_M_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "TB_M_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Return_Item_Id",
                table: "TB_T_Return",
                column: "Item_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Return_User_Id",
                table: "TB_T_Return",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_T_Return");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "TB_M_Item");

            migrationBuilder.DropColumn(
                name: "Cpu",
                table: "TB_M_Item");

            migrationBuilder.DropColumn(
                name: "Display",
                table: "TB_M_Item");

            migrationBuilder.DropColumn(
                name: "Gpu",
                table: "TB_M_Item");

            migrationBuilder.DropColumn(
                name: "Os",
                table: "TB_M_Item");

            migrationBuilder.RenameColumn(
                name: "Storage",
                table: "TB_M_Item",
                newName: "Specification");

            migrationBuilder.RenameColumn(
                name: "Ram",
                table: "TB_M_Item",
                newName: "Name");
        }
    }
}
