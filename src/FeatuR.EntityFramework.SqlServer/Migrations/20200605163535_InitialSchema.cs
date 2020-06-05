using Microsoft.EntityFrameworkCore.Migrations;

namespace FeatuR.EntityFramework.SqlServer.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 140, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    ActivationStrategies = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");
        }
    }
}
