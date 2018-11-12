using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.Data.Migrations
{
    public partial class TaskUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskUser");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TaskStatus",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TaskModerator_User",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModerator_User", x => new { x.UserId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TaskModerator_User_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskModerator_User_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskViewer_User",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskViewer_User", x => new { x.UserId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TaskViewer_User_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskViewer_User_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "ToDo" });

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "InProgress" });

            migrationBuilder.InsertData(
                table: "TaskStatus",
                columns: new[] { "Id", "Title" },
                values: new object[] { 3, "Done" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskModerator_User_TaskId",
                table: "TaskModerator_User",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskViewer_User_TaskId",
                table: "TaskViewer_User",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskModerator_User");

            migrationBuilder.DropTable(
                name: "TaskViewer_User");

            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TaskStatus",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "TaskUser",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    TaskId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskUser", x => new { x.UserId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TaskUser_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskUser_Tasks_TaskId1",
                        column: x => x.TaskId1,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskUser_TaskId",
                table: "TaskUser",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskUser_TaskId1",
                table: "TaskUser",
                column: "TaskId1");
        }
    }
}
