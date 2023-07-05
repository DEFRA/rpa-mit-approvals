using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EST.MIT.Approvals.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesBasedOnERDv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approver_scheme_grades");

            migrationBuilder.DropTable(
                name: "scheme_approval_grades");

            migrationBuilder.DropTable(
                name: "scheme_grades");

            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropColumn(
                name: "description",
                table: "schemes");

            migrationBuilder.DropColumn(
                name: "name",
                table: "schemes");

            migrationBuilder.CreateTable(
                name: "approver_scheme_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    approver_id = table.Column<int>(type: "integer", nullable: false),
                    scheme_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approver_scheme_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_approver_scheme_entity_approvers_approver_id",
                        column: x => x.approver_id,
                        principalTable: "approvers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_approver_scheme_entity_schemes_scheme_id",
                        column: x => x.scheme_id,
                        principalTable: "schemes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_approver_scheme_entity_approver_id",
                table: "approver_scheme_entity",
                column: "approver_id");

            migrationBuilder.CreateIndex(
                name: "ix_approver_scheme_entity_scheme_id",
                table: "approver_scheme_entity",
                column: "scheme_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approver_scheme_entity");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "schemes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "schemes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scheme_grades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    grade_id = table.Column<int>(type: "integer", nullable: false),
                    scheme_id = table.Column<int>(type: "integer", nullable: false),
                    approver_entity_id = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scheme_grades", x => x.id);
                    table.ForeignKey(
                        name: "fk_scheme_grades_approvers_approver_entity_id",
                        column: x => x.approver_entity_id,
                        principalTable: "approvers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_scheme_grades_grades_grade_id",
                        column: x => x.grade_id,
                        principalTable: "grades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_scheme_grades_schemes_scheme_id",
                        column: x => x.scheme_id,
                        principalTable: "schemes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "approver_scheme_grades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    approver_id = table.Column<int>(type: "integer", nullable: false),
                    scheme_grade_id = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "scheme_approval_grades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scheme_grade_id = table.Column<int>(type: "integer", nullable: false),
                    approval_limit = table.Column<decimal>(type: "numeric", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_unlimited = table.Column<bool>(type: "boolean", nullable: false),
                    modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scheme_approval_grades", x => x.id);
                    table.ForeignKey(
                        name: "fk_scheme_approval_grades_scheme_grades_scheme_grade_id",
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

            migrationBuilder.CreateIndex(
                name: "ix_scheme_approval_grades_scheme_grade_id",
                table: "scheme_approval_grades",
                column: "scheme_grade_id");

            migrationBuilder.CreateIndex(
                name: "ix_scheme_grades_approver_entity_id",
                table: "scheme_grades",
                column: "approver_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_scheme_grades_grade_id",
                table: "scheme_grades",
                column: "grade_id");

            migrationBuilder.CreateIndex(
                name: "ix_scheme_grades_scheme_id",
                table: "scheme_grades",
                column: "scheme_id");
        }
    }
}
