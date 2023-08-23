using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserConnectionTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_PostEntity_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_PostEntity_PostId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_PostEntity_UserEntity_UserId",
                table: "PostEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostEntity",
                table: "PostEntity");

            migrationBuilder.RenameTable(
                name: "PostEntity",
                newName: "Posts");

            migrationBuilder.RenameIndex(
                name: "IX_PostEntity_UserId",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserConnectionMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ConnectionId = table.Column<string>(type: "text", nullable: true),
                    UserEntityId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnectionMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConnectionMappings_UserEntity_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserConnectionMappings_UserEntityId",
                table: "UserConnectionMappings",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Posts_PostId",
                table: "Like",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_UserEntity_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "UserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Posts_PostId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_UserEntity_UserId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "UserConnectionMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "PostEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "PostEntity",
                newName: "IX_PostEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostEntity",
                table: "PostEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_PostEntity_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "PostEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_PostEntity_PostId",
                table: "Like",
                column: "PostId",
                principalTable: "PostEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntity_UserEntity_UserId",
                table: "PostEntity",
                column: "UserId",
                principalTable: "UserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
