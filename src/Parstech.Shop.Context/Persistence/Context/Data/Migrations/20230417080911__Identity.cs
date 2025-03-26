using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parstech.Shop.Context.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class _Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "IUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "IUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "IUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "IUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "IUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "IRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "IRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "IUserRoles",
                newName: "IX_IUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "IUserLogins",
                newName: "IX_IUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "IUserClaims",
                newName: "IX_IUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "IRoleClaims",
                newName: "IX_IRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IUserTokens",
                table: "IUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_IUsers",
                table: "IUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IUserRoles",
                table: "IUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_IUserLogins",
                table: "IUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_IUserClaims",
                table: "IUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IRoles",
                table: "IRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IRoleClaims",
                table: "IRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IRoleClaims_IRoles_RoleId",
                table: "IRoleClaims",
                column: "RoleId",
                principalTable: "IRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IUserClaims_IUsers_UserId",
                table: "IUserClaims",
                column: "UserId",
                principalTable: "IUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IUserLogins_IUsers_UserId",
                table: "IUserLogins",
                column: "UserId",
                principalTable: "IUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IUserRoles_IRoles_RoleId",
                table: "IUserRoles",
                column: "RoleId",
                principalTable: "IRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IUserRoles_IUsers_UserId",
                table: "IUserRoles",
                column: "UserId",
                principalTable: "IUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IUserTokens_IUsers_UserId",
                table: "IUserTokens",
                column: "UserId",
                principalTable: "IUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IRoleClaims_IRoles_RoleId",
                table: "IRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_IUserClaims_IUsers_UserId",
                table: "IUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_IUserLogins_IUsers_UserId",
                table: "IUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_IUserRoles_IRoles_RoleId",
                table: "IUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_IUserRoles_IUsers_UserId",
                table: "IUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_IUserTokens_IUsers_UserId",
                table: "IUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IUserTokens",
                table: "IUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IUsers",
                table: "IUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IUserRoles",
                table: "IUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IUserLogins",
                table: "IUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IUserClaims",
                table: "IUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IRoles",
                table: "IRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IRoleClaims",
                table: "IRoleClaims");

            migrationBuilder.RenameTable(
                name: "IUserTokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "IUsers",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "IUserRoles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "IUserLogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "IUserClaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "IRoles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "IRoleClaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_IUserRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_IUserLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IUserClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
