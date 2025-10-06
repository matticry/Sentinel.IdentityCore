using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sentinel.Identity.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "tbl_user",
                schema: "public",
                columns: table => new
                {
                    id_us = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_us = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lastname_us = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    dni_us = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: false),
                    phone_us = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address_us = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    age_us = table.Column<int>(type: "integer", nullable: false),
                    password_us = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    username_us = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email_us = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email_verified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status_us = table.Column<char>(type: "character(1)", fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.id_us);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user_audit_delete",
                schema: "public",
                columns: table => new
                {
                    audit_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_us = table.Column<int>(type: "integer", nullable: false),
                    name_us = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lastname_us = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    dni_us = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: false),
                    phone_us = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address_us = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status_us = table.Column<char>(type: "character(1)", fixedLength: true, maxLength: 1, nullable: false),
                    age_us = table.Column<int>(type: "integer", nullable: false),
                    username_us = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email_us = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    deleted_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user_audit_delete", x => x.audit_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user_audit_update",
                schema: "public",
                columns: table => new
                {
                    audit_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_us = table.Column<int>(type: "integer", nullable: false),
                    field_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    old_value = table.Column<string>(type: "TEXT", nullable: true),
                    new_value = table.Column<string>(type: "TEXT", nullable: true),
                    changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    changed_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user_audit_update", x => x.audit_id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_dni",
                schema: "public",
                table: "tbl_user",
                column: "dni_us",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_email",
                schema: "public",
                table: "tbl_user",
                column: "email_us",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_username",
                schema: "public",
                table: "tbl_user",
                column: "username_us",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_audit_delete_user",
                schema: "public",
                table: "tbl_user_audit_delete",
                column: "id_us");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_audit_update_user",
                schema: "public",
                table: "tbl_user_audit_update",
                column: "id_us");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tbl_user_audit_delete",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tbl_user_audit_update",
                schema: "public");
        }
    }
}
