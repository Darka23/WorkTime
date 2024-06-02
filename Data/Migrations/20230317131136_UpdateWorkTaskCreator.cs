using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTime.Data.Migrations
{
    public partial class UpdateWorkTaskCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_AspNetUsers_CreatorId",
                table: "WorkTasks");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "WorkTasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_AspNetUsers_CreatorId",
                table: "WorkTasks",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_AspNetUsers_CreatorId",
                table: "WorkTasks");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "WorkTasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_AspNetUsers_CreatorId",
                table: "WorkTasks",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
