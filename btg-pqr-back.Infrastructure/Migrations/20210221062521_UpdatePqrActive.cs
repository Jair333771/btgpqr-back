using Microsoft.EntityFrameworkCore.Migrations;

namespace btg_pqr_back.Infrastructure.Migrations
{
    public partial class UpdatePqrActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "pqrs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "pqrs");
        }
    }
}
