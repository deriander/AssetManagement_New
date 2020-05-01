using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.Migrations
{
    public partial class addAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Specification = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Is_Delete = table.Column<bool>(nullable: false),
                    Create_Date = table.Column<DateTimeOffset>(nullable: false),
                    Update_Date = table.Column<DateTimeOffset>(nullable: true),
                    Delete_Date = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    First_Name = table.Column<string>(nullable: true),
                    Last_Name = table.Column<string>(nullable: true),
                    Phone_Number = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Birth_Date = table.Column<DateTime>(nullable: false),
                    Role_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_Request",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Item_Name = table.Column<string>(nullable: true),
                    Approval_1 = table.Column<bool>(nullable: false),
                    Approval_2 = table.Column<bool>(nullable: false),
                    Specification = table.Column<string>(nullable: true),
                    Status_Approval = table.Column<string>(nullable: true),
                    User_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_T_Request_TB_M_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "TB_M_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Request_User_Id",
                table: "TB_T_Request",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_Item");

            migrationBuilder.DropTable(
                name: "TB_T_Request");

            migrationBuilder.DropTable(
                name: "TB_M_User");
        }
    }
}
