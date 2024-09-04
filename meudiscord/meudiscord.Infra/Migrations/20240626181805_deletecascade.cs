using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace meudiscord.Infra.Migrations
{
    /// <inheritdoc />
    public partial class deletecascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "externalId",
                table: "users",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(200)",
                oldMaxLength: 200)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "externalId",
                table: "tokens",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(200)",
                oldMaxLength: 200)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "externalId",
                table: "servers",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(200)",
                oldMaxLength: 200)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "externalId",
                table: "messages",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(200)",
                oldMaxLength: 200)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "externalId",
                table: "channels",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(200)",
                oldMaxLength: 200)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "externalId",
                table: "users",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "externalId",
                table: "tokens",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "externalId",
                table: "servers",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "externalId",
                table: "messages",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "externalId",
                table: "channels",
                type: "char(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
