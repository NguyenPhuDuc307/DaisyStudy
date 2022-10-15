using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaisyStudy.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppConfigs",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfigs", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_ID = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassRoom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SEOClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SEODescriptione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SEOAlias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tuition = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isPublic = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Contact_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Contact_ID);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Transaction_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExternalTransactionID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Transaction_ID);
                    table.ForeignKey(
                        name: "FK_Transactions_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Chat_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatetimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Dislikes = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Chat_ID);
                    table.ForeignKey(
                        name: "FK_Chats_AppUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Classes_Class_ID",
                        column: x => x.Class_ID,
                        principalTable: "Classes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassDetails",
                columns: table => new
                {
                    Class_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassDetails", x => new { x.Class_ID, x.User_ID });
                    table.ForeignKey(
                        name: "FK_ClassDetails_AppUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassDetails_Classes_Class_ID",
                        column: x => x.Class_ID,
                        principalTable: "Classes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamSchedules",
                columns: table => new
                {
                    ExamSchedule_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_ID = table.Column<int>(type: "int", nullable: false),
                    ExamScheduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatetimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamTime = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSchedules", x => x.ExamSchedule_ID);
                    table.ForeignKey(
                        name: "FK_ExamSchedules_Classes_Class_ID",
                        column: x => x.Class_ID,
                        principalTable: "Classes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    Homework_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_ID = table.Column<int>(type: "int", nullable: false),
                    HomeworkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatetimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Homework_ID);
                    table.ForeignKey(
                        name: "FK_Homeworks_Classes_Class_ID",
                        column: x => x.Class_ID,
                        principalTable: "Classes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Notification_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_ID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatetimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Notification_ID);
                    table.ForeignKey(
                        name: "FK_Notifications_Classes_Class_ID",
                        column: x => x.Class_ID,
                        principalTable: "Classes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Question_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamSchedule_ID = table.Column<int>(type: "int", nullable: false),
                    QuestionString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Point = table.Column<float>(type: "real", nullable: false, defaultValue: 0f)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Question_ID);
                    table.ForeignKey(
                        name: "FK_Questions_ExamSchedules_ExamSchedule_ID",
                        column: x => x.ExamSchedule_ID,
                        principalTable: "ExamSchedules",
                        principalColumn: "ExamSchedule_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentExams",
                columns: table => new
                {
                    StudentExam_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamSchedule_ID = table.Column<int>(type: "int", nullable: false),
                    Student_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mark = table.Column<float>(type: "real", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExams", x => x.StudentExam_ID);
                    table.ForeignKey(
                        name: "FK_StudentExams_AppUsers_Student_ID",
                        column: x => x.Student_ID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentExams_ExamSchedules_ExamSchedule_ID",
                        column: x => x.ExamSchedule_ID,
                        principalTable: "ExamSchedules",
                        principalColumn: "ExamSchedule_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Homework_ID = table.Column<int>(type: "int", nullable: false),
                    Student_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mark = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeSubmission = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => new { x.Homework_ID, x.Student_ID });
                    table.ForeignKey(
                        name: "FK_Submissions_AppUsers_Student_ID",
                        column: x => x.Student_ID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_Homeworks_Homework_ID",
                        column: x => x.Homework_ID,
                        principalTable: "Homeworks",
                        principalColumn: "Homework_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Comment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notification_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatetimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Dislikes = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Comment_ID);
                    table.ForeignKey(
                        name: "FK_Comments_AppUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Notifications_Notification_ID",
                        column: x => x.Notification_ID,
                        principalTable: "Notifications",
                        principalColumn: "Notification_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Answer_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question_ID = table.Column<int>(type: "int", nullable: false),
                    AnswerString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Answer_ID);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_Question_ID",
                        column: x => x.Question_ID,
                        principalTable: "Questions",
                        principalColumn: "Question_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentExamDetails",
                columns: table => new
                {
                    StudentExamDetail_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentExam_ID = table.Column<int>(type: "int", nullable: false),
                    Answer_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamDetails", x => x.StudentExamDetail_ID);
                    table.ForeignKey(
                        name: "FK_StudentExamDetails_Answers_Answer_ID",
                        column: x => x.Answer_ID,
                        principalTable: "Answers",
                        principalColumn: "Answer_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentExamDetails_StudentExams_StudentExam_ID",
                        column: x => x.StudentExam_ID,
                        principalTable: "StudentExams",
                        principalColumn: "StudentExam_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Question_ID",
                table: "Answers",
                column: "Question_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_Class_ID",
                table: "Chats",
                column: "Class_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_User_ID",
                table: "Chats",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassDetails_User_ID",
                table: "ClassDetails",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Notification_ID",
                table: "Comments",
                column: "Notification_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_User_ID",
                table: "Comments",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSchedules_Class_ID",
                table: "ExamSchedules",
                column: "Class_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_Class_ID",
                table: "Homeworks",
                column: "Class_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Class_ID",
                table: "Notifications",
                column: "Class_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExamSchedule_ID",
                table: "Questions",
                column: "ExamSchedule_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamDetails_Answer_ID",
                table: "StudentExamDetails",
                column: "Answer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamDetails_StudentExam_ID",
                table: "StudentExamDetails",
                column: "StudentExam_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_ExamSchedule_ID",
                table: "StudentExams",
                column: "ExamSchedule_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_Student_ID",
                table: "StudentExams",
                column: "Student_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_Student_ID",
                table: "Submissions",
                column: "Student_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfigs");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "ClassDetails");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "StudentExamDetails");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "StudentExams");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "ExamSchedules");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
