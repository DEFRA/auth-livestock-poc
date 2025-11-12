using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livestock.Auth.Database.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "defra-ci");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "application",
                schema: "defra-ci",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    b2c_client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    b2c_tenant = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "active"),
                    created_at = table.Column<DateTime>(type: "TimestampTz", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "TimestampTz", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "enrolment",
                schema: "defra-ci",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    b2c_object_id = table.Column<Guid>(type: "uuid", nullable: false),
                    application_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cph_id = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    scope = table.Column<string>(type: "jsonb", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "active"),
                    enrolled_at = table.Column<DateTime>(type: "TimestampTz", nullable: false, defaultValueSql: "now()"),
                    expires_at = table.Column<DateTime>(type: "TimestampTz", nullable: false),
                    created_at = table.Column<DateTime>(type: "TimestampTz", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "TimestampTz", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrolment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_account",
                schema: "defra-ci",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    upn = table.Column<string>(type: "citext", nullable: false),
                    display_name = table.Column<string>(type: "varchar", maxLength: 256, nullable: false),
                    account_enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "TimestampTz", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "TimestampTz", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "federation",
                schema: "defra-ci",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    b2c_tenant = table.Column<string>(type: "text", nullable: false),
                    b2c_object_id = table.Column<Guid>(type: "uuid", nullable: false),
                    trust_level = table.Column<string>(type: "text", nullable: false, defaultValue: "standard"),
                    sync_status = table.Column<string>(type: "text", nullable: false, defaultValue: "linked"),
                    last_synced_at = table.Column<DateTime>(type: "TimestampTz", nullable: false),
                    created_at = table.Column<DateTime>(type: "TimestampTz", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "TimestampTz", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_federation", x => x.id);
                    table.ForeignKey(
                        name: "FK_federation_user_account_user_account_id",
                        column: x => x.user_account_id,
                        principalSchema: "defra-ci",
                        principalTable: "user_account",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_enrolment_b2c_object_id_role",
                schema: "defra-ci",
                table: "enrolment",
                columns: new[] { "b2c_object_id", "role" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_federation_b2c_object_id_b2c_tenant",
                schema: "defra-ci",
                table: "federation",
                columns: new[] { "b2c_object_id", "b2c_tenant" });

            migrationBuilder.CreateIndex(
                name: "IX_federation_user_account_id",
                schema: "defra-ci",
                table: "federation",
                column: "user_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_account_upn",
                schema: "defra-ci",
                table: "user_account",
                column: "upn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "application",
                schema: "defra-ci");

            migrationBuilder.DropTable(
                name: "enrolment",
                schema: "defra-ci");

            migrationBuilder.DropTable(
                name: "federation",
                schema: "defra-ci");

            migrationBuilder.DropTable(
                name: "user_account",
                schema: "defra-ci");
        }
    }
}
