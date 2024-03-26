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

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.Id);
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
            migrationBuilder.Sql(@"
INSERT INTO ""UserLogin"" (""UserName"", ""Password"", ""Salt"", ""Created"", ""Updated"") VALUES
('Admin', decode('4EC0BBBD31968047B5D743561B20687430F80503CF94CEF3A763BC65C2196783B4171405AD4EE2FAD66B48AACE02C0F1D12F8E223A6CE8C0B9F5B3FF0F9EA5D3', 'hex'), decode('D2A6DAFFA332E1AC85F1BFC8B5D8794DDE658FCD996D133CEAAD13AB025A953954C266F49EE40518F048A22DE2A3C4422EE9C77CBB6FF31C4583EA7BE63C0785', 'hex'), NOW(), NOW());");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserLogin");
        }
    }
}
