using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.Migrations
{
    public partial class editRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "TB_T_Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpu",
                table: "TB_T_Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Display",
                table: "TB_T_Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gpu",
                table: "TB_T_Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Os",
                table: "TB_T_Request",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ram",
                table: "TB_T_Request",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Request_Date",
                table: "TB_T_Request",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Storage",
                table: "TB_T_Request",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_T_Request_Specification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(nullable: true),
                    Cpu = table.Column<string>(nullable: true),
                    Gpu = table.Column<string>(nullable: true),
                    Ram = table.Column<string>(nullable: true),
                    Display = table.Column<string>(nullable: true),
                    Storage = table.Column<string>(nullable: true),
                    Os = table.Column<string>(nullable: true),
                    Request_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_Request_Specification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_T_Request_Specification_TB_T_Request_Request_Id",
                        column: x => x.Request_Id,
                        principalTable: "TB_T_Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Request_Specification_Request_Id",
                table: "TB_T_Request_Specification",
                column: "Request_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_T_Request_Specification");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "TB_T_Request");

            migrationBuilder.DropColumn(
                name: "Cpu",
                table: "TB_T_Request");

            migrationBuilder.DropColumn(
                name: "Display",
                table: "TB_T_Request");

            migrationBuilder.DropColumn(
                name: "Gpu",
                table: "TB_T_Request");

            migrationBuilder.DropColumn(
                name: "Os",
                table: "TB_T_Request");

            migrationBuilder.DropColumn(
                name: "Ram",
                table: "TB_T_Request");

            migrationBuilder.DropColumn(
                name: "Request_Date",
                table: "TB_T_Request");

            migrationBuilder.DropColumn(
                name: "Storage",
                table: "TB_T_Request");
        }
    }
}
