using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersServiceApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDetails_UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDetails_ProfileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDetails_ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "LikedVideos",
                columns: table => new
                {
                    LikedVideoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LikedVideoDetails_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LikedVideoDetails_VideoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedVideos", x => x.LikedVideoId);
                    table.ForeignKey(
                        name: "FK_LikedVideos_Users_LikedVideoDetails_UserId",
                        column: x => x.LikedVideoDetails_UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscriptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubDetails_SubscriberUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubDetails_SubscribingToUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.SubscriptionId);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_SubDetails_SubscriberUserId",
                        column: x => x.SubDetails_SubscriberUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_SubDetails_SubscribingToUserId",
                        column: x => x.SubDetails_SubscribingToUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedVideos_LikedVideoDetails_UserId",
                table: "LikedVideos",
                column: "LikedVideoDetails_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubDetails_SubscriberUserId",
                table: "Subscriptions",
                column: "SubDetails_SubscriberUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubDetails_SubscribingToUserId",
                table: "Subscriptions",
                column: "SubDetails_SubscribingToUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikedVideos");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
