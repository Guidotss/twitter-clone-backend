using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdAndTweetIdFromCommentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Tweet_TweetId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Follow_User_FolloweeId",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "FK_Follow_User_FollowerId",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Tweet_TweetId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_User_UserId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Retweet_Tweet_TweetId",
                table: "Retweet");

            migrationBuilder.DropForeignKey(
                name: "FK_Retweet_User_UserId",
                table: "Retweet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Retweet",
                table: "Retweet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follow",
                table: "Follow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Retweet",
                newName: "Retweets");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.RenameTable(
                name: "Follow",
                newName: "Follows");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Retweet_UserId",
                table: "Retweets",
                newName: "IX_Retweets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Retweet_TweetId",
                table: "Retweets",
                newName: "IX_Retweets_TweetId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_TweetId",
                table: "Likes",
                newName: "IX_Likes_TweetId");

            migrationBuilder.RenameIndex(
                name: "IX_Follow_FollowerId",
                table: "Follows",
                newName: "IX_Follows_FollowerId");

            migrationBuilder.RenameIndex(
                name: "IX_Follow_FolloweeId",
                table: "Follows",
                newName: "IX_Follows_FolloweeId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_TweetId",
                table: "Comments",
                newName: "IX_Comments_TweetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Retweets",
                table: "Retweets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follows",
                table: "Follows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tweet_TweetId",
                table: "Comments",
                column: "TweetId",
                principalTable: "Tweet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_User_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_User_FolloweeId",
                table: "Follows",
                column: "FolloweeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_User_FollowerId",
                table: "Follows",
                column: "FollowerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Tweet_TweetId",
                table: "Likes",
                column: "TweetId",
                principalTable: "Tweet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_User_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Retweets_Tweet_TweetId",
                table: "Retweets",
                column: "TweetId",
                principalTable: "Tweet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Retweets_User_UserId",
                table: "Retweets",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tweet_TweetId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_User_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_User_FolloweeId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_User_FollowerId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Tweet_TweetId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_User_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Retweets_Tweet_TweetId",
                table: "Retweets");

            migrationBuilder.DropForeignKey(
                name: "FK_Retweets_User_UserId",
                table: "Retweets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Retweets",
                table: "Retweets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follows",
                table: "Follows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Retweets",
                newName: "Retweet");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.RenameTable(
                name: "Follows",
                newName: "Follow");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Retweets_UserId",
                table: "Retweet",
                newName: "IX_Retweet_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Retweets_TweetId",
                table: "Retweet",
                newName: "IX_Retweet_TweetId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "Like",
                newName: "IX_Like_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_TweetId",
                table: "Like",
                newName: "IX_Like_TweetId");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_FollowerId",
                table: "Follow",
                newName: "IX_Follow_FollowerId");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_FolloweeId",
                table: "Follow",
                newName: "IX_Follow_FolloweeId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TweetId",
                table: "Comment",
                newName: "IX_Comment_TweetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Retweet",
                table: "Retweet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follow",
                table: "Follow",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Tweet_TweetId",
                table: "Comment",
                column: "TweetId",
                principalTable: "Tweet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_User_FolloweeId",
                table: "Follow",
                column: "FolloweeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_User_FollowerId",
                table: "Follow",
                column: "FollowerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Tweet_TweetId",
                table: "Like",
                column: "TweetId",
                principalTable: "Tweet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_User_UserId",
                table: "Like",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Retweet_Tweet_TweetId",
                table: "Retweet",
                column: "TweetId",
                principalTable: "Tweet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Retweet_User_UserId",
                table: "Retweet",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
