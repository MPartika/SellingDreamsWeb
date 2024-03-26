using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SellingDreamsInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EmailAdress = table.Column<string>(type: "text", nullable: false),
                    Adress = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => new { x.Id, x.Name, x.EmailAdress });
                });

            migrationBuilder.Sql(@"
INSERT INTO ""User"" (""Name"", ""Adress"", ""EmailAdress"", ""PhoneNumber"", ""Created"", ""Updated"")
VALUES ('Jennifer Wilson', '542 Elm Street', 'jenwilson@example.com', '555-1234', NOW(), NOW())
,('Mark Thomas', null, 'markthomas@example.com', NULL, NOW(), NOW())
,('David Lee', NULL, 'davidlee@example.com', NULL, NOW(), NOW())
,('Emily Jones', NULL, '123 Main Street', '555-9876', NOW(), NOW())
,('Robert Brown', '456 Oak Avenue', 'robertbrown@example.com', NULL, NOW(), NOW())
,('Maria Garcia', NULL, 'mariagarcia@example.com', NULL, NOW(), NOW())
,('John Smith', NULL, '789 Pine Street', '555-5678', NOW(), NOW())
,('Lisa Miller', NULL, 'lisamiller@example.com', '555-4321', NOW(), NOW())
,('William Johnson', '901 Maple Drive', 'williamjohnson@example.com', NULL, NOW(), NOW())
,('Susan Jackson', NULL, 'susanjackson@example.com', NULL, NOW(), NOW())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
