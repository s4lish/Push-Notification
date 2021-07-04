using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PushNotification.Migrations
{
    public partial class HelloPush : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    user = table.Column<string>(nullable: true),
                    keyuser = table.Column<Guid>(nullable: false),
                    datetime = table.Column<DateTime>(nullable: false),
                    status = table.Column<bool>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    attach1 = table.Column<string>(nullable: true),
                    attach2 = table.Column<string>(nullable: true),
                    attach3 = table.Column<string>(nullable: true),
                    attach4 = table.Column<string>(nullable: true),
                    attach5 = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Reserve1 = table.Column<string>(nullable: true),
                    Reserve2 = table.Column<string>(nullable: true),
                    Reserve3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
