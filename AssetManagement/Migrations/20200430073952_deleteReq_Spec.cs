using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.Migrations
{
    public partial class deleteReq_Spec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_T_Request_Specification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_T_Request_Specification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(nullable: true),
                    Cpu = table.Column<string>(nullable: true),
                    Display = table.Column<string>(nullable: true),
                    Gpu = table.Column<string>(nullable: true),
                    Os = table.Column<string>(nullable: true),
                    Ram = table.Column<string>(nullable: true),
                    Request_Id = table.Column<int>(nullable: false),
                    Storage = table.Column<string>(nullable: true)
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
    }
}
