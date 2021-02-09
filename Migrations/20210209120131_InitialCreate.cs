using Microsoft.EntityFrameworkCore.Migrations;

namespace HelloWorldFromDB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HELLO_WORLD");

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "HELLO_WORLD",
                columns: table => new
                {
                    Author = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Author);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages",
                schema: "HELLO_WORLD");
        }
    }
}
