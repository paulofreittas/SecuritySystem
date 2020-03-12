using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecuritySystem.Repositories.Migrations
{
    // As migrations são usadas para controlar as alterações do banco de dados, como foi usado a técnica de code first.
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "system",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(maxLength: 100, nullable: false),
                    initials = table.Column<string>(maxLength: 10, nullable: false),
                    email = table.Column<string>(maxLength: 100, nullable: true),
                    url = table.Column<string>(maxLength: 50, nullable: true),
                    status = table.Column<int>(nullable: false),
                    user_responsible_for_last_update = table.Column<string>(maxLength: 100, nullable: true),
                    update_at = table.Column<DateTime>(nullable: false),
                    justification_for_the_last_update = table.Column<string>(maxLength: 500, nullable: true),
                    new_justification = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system");
        }
    }
}
