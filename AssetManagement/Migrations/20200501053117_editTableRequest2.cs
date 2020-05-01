using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.Migrations
{
    public partial class editTableRequest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item_Name",
                table: "TB_T_Request");

            migrationBuilder.DropColumn(
                name: "Specification",
                table: "TB_T_Request");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Item_Name",
                table: "TB_T_Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specification",
                table: "TB_T_Request",
                nullable: true);
        }
    }
}
