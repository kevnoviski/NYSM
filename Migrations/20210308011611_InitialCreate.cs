using Microsoft.EntityFrameworkCore.Migrations;

namespace NYSM.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LineCount = table.Column<int>(type: "int", nullable: false),
                    WordCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "readSpeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordPerSecond = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_readSpeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppFileId = table.Column<int>(type: "int", nullable: true),
                    QuestionContent = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CorretAnwser = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Alternative1 = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Alternative2 = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Alternative3 = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Alternative4 = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questions_appFiles_AppFileId",
                        column: x => x.AppFileId,
                        principalTable: "appFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "testConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AppFileId = table.Column<int>(type: "int", nullable: true),
                    ReadSpeedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_testConfigs_appFiles_AppFileId",
                        column: x => x.AppFileId,
                        principalTable: "appFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_testConfigs_readSpeeds_ReadSpeedId",
                        column: x => x.ReadSpeedId,
                        principalTable: "readSpeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_testConfigs_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "testResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TestcCnfigId = table.Column<int>(type: "int", nullable: true),
                    Percentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_testResults_testConfigs_TestcCnfigId",
                        column: x => x.TestcCnfigId,
                        principalTable: "testConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_testResults_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_questions_AppFileId",
                table: "questions",
                column: "AppFileId");

            migrationBuilder.CreateIndex(
                name: "IX_testConfigs_AppFileId",
                table: "testConfigs",
                column: "AppFileId");

            migrationBuilder.CreateIndex(
                name: "IX_testConfigs_ReadSpeedId",
                table: "testConfigs",
                column: "ReadSpeedId");

            migrationBuilder.CreateIndex(
                name: "IX_testConfigs_UserId",
                table: "testConfigs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_testResults_TestcCnfigId",
                table: "testResults",
                column: "TestcCnfigId");

            migrationBuilder.CreateIndex(
                name: "IX_testResults_UserId",
                table: "testResults",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "testResults");

            migrationBuilder.DropTable(
                name: "testConfigs");

            migrationBuilder.DropTable(
                name: "appFiles");

            migrationBuilder.DropTable(
                name: "readSpeeds");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
