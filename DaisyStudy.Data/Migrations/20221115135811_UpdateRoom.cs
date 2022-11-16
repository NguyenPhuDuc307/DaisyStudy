using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaisyStudy.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AppUsers_FromUserID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_RoomChats_ToRoomID",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "RoomChats");

            migrationBuilder.RenameColumn(
                name: "ToRoomID",
                table: "Messages",
                newName: "ToRoomId");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Messages",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "FromUserID",
                table: "Messages",
                newName: "FromUserId");

            migrationBuilder.RenameColumn(
                name: "MessageID",
                table: "Messages",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ToRoomID",
                table: "Messages",
                newName: "IX_Messages_ToRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_FromUserID",
                table: "Messages",
                newName: "IX_Messages_FromUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "FromUserId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_AppUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AdminId",
                table: "Rooms",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AppUsers_FromUserId",
                table: "Messages",
                column: "FromUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Rooms_ToRoomId",
                table: "Messages",
                column: "ToRoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AppUsers_FromUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Rooms_ToRoomId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.RenameColumn(
                name: "ToRoomId",
                table: "Messages",
                newName: "ToRoomID");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Messages",
                newName: "TimeStamp");

            migrationBuilder.RenameColumn(
                name: "FromUserId",
                table: "Messages",
                newName: "FromUserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Messages",
                newName: "MessageID");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ToRoomId",
                table: "Messages",
                newName: "IX_Messages_ToRoomID");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_FromUserId",
                table: "Messages",
                newName: "IX_Messages_FromUserID");

            migrationBuilder.AlterColumn<Guid>(
                name: "FromUserID",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "RoomChats",
                columns: table => new
                {
                    RoomChatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomChatName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomChats", x => x.RoomChatID);
                    table.ForeignKey(
                        name: "FK_RoomChats_AppUsers_AdminID",
                        column: x => x.AdminID,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomChats_AdminID",
                table: "RoomChats",
                column: "AdminID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AppUsers_FromUserID",
                table: "Messages",
                column: "FromUserID",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_RoomChats_ToRoomID",
                table: "Messages",
                column: "ToRoomID",
                principalTable: "RoomChats",
                principalColumn: "RoomChatID");
        }
    }
}
