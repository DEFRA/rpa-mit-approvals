using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EST.MIT.Approvals.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class UpdateEntitiesBasedOnERD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by_user_id",
                table: "schemes");

            migrationBuilder.DropColumn(
                name: "modified_by_user_id",
                table: "schemes");

            migrationBuilder.DropColumn(
                name: "created_by_user_id",
                table: "scheme_grades");

            migrationBuilder.DropColumn(
                name: "modified_by_user_id",
                table: "scheme_grades");

            migrationBuilder.DropColumn(
                name: "created_by_user_id",
                table: "scheme_approval_grades");

            migrationBuilder.DropColumn(
                name: "modified_by_user_id",
                table: "scheme_approval_grades");

            migrationBuilder.DropColumn(
                name: "created_by_user_id",
                table: "grades");

            migrationBuilder.DropColumn(
                name: "modified_by_user_id",
                table: "grades");

            migrationBuilder.DropColumn(
                name: "created_by_user_id",
                table: "approvers");

            migrationBuilder.DropColumn(
                name: "modified_by_user_id",
                table: "approvers");

            migrationBuilder.CreateTable(
                name: "approver_scheme_grades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    approver_id = table.Column<int>(type: "integer", nullable: false),
                    scheme_grade_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approver_scheme_grades", x => x.id);
                    table.ForeignKey(
                        name: "fk_approver_scheme_grades_approvers_approver_id",
                        column: x => x.approver_id,
                        principalTable: "approvers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_approver_scheme_grades_scheme_grades_scheme_grade_id",
                        column: x => x.scheme_grade_id,
                        principalTable: "scheme_grades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_approver_scheme_grades_approver_id",
                table: "approver_scheme_grades",
                column: "approver_id");

            migrationBuilder.CreateIndex(
                name: "ix_approver_scheme_grades_scheme_grade_id",
                table: "approver_scheme_grades",
                column: "scheme_grade_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approver_scheme_grades");

            migrationBuilder.AddColumn<int>(
                name: "created_by_user_id",
                table: "schemes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "modified_by_user_id",
                table: "schemes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "created_by_user_id",
                table: "scheme_grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "modified_by_user_id",
                table: "scheme_grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "created_by_user_id",
                table: "scheme_approval_grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "modified_by_user_id",
                table: "scheme_approval_grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "created_by_user_id",
                table: "grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "modified_by_user_id",
                table: "grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "created_by_user_id",
                table: "approvers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "modified_by_user_id",
                table: "approvers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
