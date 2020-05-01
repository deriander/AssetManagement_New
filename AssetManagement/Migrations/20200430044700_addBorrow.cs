using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.Migrations
{
    public partial class addBorrow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_T_Borrow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Borrow_Date = table.Column<DateTimeOffset>(nullable: false),
                    Approval_1 = table.Column<bool>(nullable: false),
                    Approval_2 = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    User_Id = table.Column<int>(nullable: false),
                    Item_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_Borrow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_T_Borrow_TB_M_Item_Item_Id",
                        column: x => x.Item_Id,
                        principalTable: "TB_M_Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_T_Borrow_TB_M_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "TB_M_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Borrow_Item_Id",
                table: "TB_T_Borrow",
                column: "Item_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Borrow_User_Id",
                table: "TB_T_Borrow",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_T_Borrow");
        }
    }
}
