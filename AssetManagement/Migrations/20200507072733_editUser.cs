using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.Migrations
{
    public partial class editUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role_Id",
                table: "TB_M_User");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "TB_M_User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "TB_M_User");

            migrationBuilder.AddColumn<int>(
                name: "Role_Id",
                table: "TB_M_User",
                nullable: false,
                defaultValue: 0);
        }
    }
}
