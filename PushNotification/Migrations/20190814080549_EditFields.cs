using Microsoft.EntityFrameworkCore.Migrations;

namespace PushNotification.Migrations
{
    public partial class EditFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "attach5",
                table: "Notifications",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "attach4",
                table: "Notifications",
                newName: "Sound");

            migrationBuilder.RenameColumn(
                name: "attach3",
                table: "Notifications",
                newName: "Color");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Notifications",
                newName: "attach5");

            migrationBuilder.RenameColumn(
                name: "Sound",
                table: "Notifications",
                newName: "attach4");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Notifications",
                newName: "attach3");
        }
    }
}
