using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecuritySystem.Repositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "system",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(maxLength: 100, nullable: false),
                    initials = table.Column<string>(maxLength: 10, nullable: false),
                    email = table.Column<string>(maxLength: 100, nullable: true),
                    url = table.Column<string>(maxLength: 50, nullable: true),
                    status = table.Column<int>(nullable: false),
                    user_responsible_for_last_update = table.Column<string>(maxLength: 100, nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: false),
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
