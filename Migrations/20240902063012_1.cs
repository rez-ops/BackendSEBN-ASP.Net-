using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    Machine_zone = table.Column<string>(type: "text", nullable: false),
                    QuantiteDemander = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: true),
                    CostCenter = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeConges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeConges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: false),
                    DebutHeure = table.Column<string>(type: "text", nullable: false),
                    FinHeure = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeShifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    Matricule = table.Column<int>(type: "integer", nullable: false),
                    Shift = table.Column<char>(type: "character(1)", nullable: false),
                    Direction = table.Column<string>(type: "text", nullable: false),
                    Zone = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    TotalLeaveDaysTaken = table.Column<int>(type: "integer", nullable: false),
                    MaxLeaveDaysPerYear = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BadgeManquants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Entre = table.Column<string>(type: "text", nullable: false),
                    Sortie = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadgeManquants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BadgeManquants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangementHoraires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IdTypeShift = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangementHoraires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangementHoraires_TypeShifts_IdTypeShift",
                        column: x => x.IdTypeShift,
                        principalTable: "TypeShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangementHoraires_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeBon = table.Column<string>(type: "text", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TypePiece = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comandes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comandes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comandes_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateSortie = table.Column<DateOnly>(type: "date", nullable: false),
                    DateEntre = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IdTypeConge = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conges_TypeConges_IdTypeConge",
                        column: x => x.IdTypeConge,
                        principalTable: "TypeConges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conges_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeAdjustments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    duHeure = table.Column<string>(type: "text", nullable: true),
                    auHeure = table.Column<string>(type: "text", nullable: true),
                    IdTypeShift = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeAdjustments_TypeShifts_IdTypeShift",
                        column: x => x.IdTypeShift,
                        principalTable: "TypeShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeAdjustments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComandeArticles",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "integer", nullable: false),
                    ComandeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandeArticles", x => new { x.ArticlesId, x.ComandeId });
                    table.ForeignKey(
                        name: "FK_ComandeArticles_articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandeArticles_comandes_ComandeId",
                        column: x => x.ComandeId,
                        principalTable: "comandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Componsations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componsations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Componsations_TimeAdjustments_Id",
                        column: x => x.Id,
                        principalTable: "TimeAdjustments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_TimeAdjustments_Id",
                        column: x => x.Id,
                        principalTable: "TimeAdjustments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BadgeManquants_UserId",
                table: "BadgeManquants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangementHoraires_IdTypeShift",
                table: "ChangementHoraires",
                column: "IdTypeShift");

            migrationBuilder.CreateIndex(
                name: "IX_ChangementHoraires_UserId",
                table: "ChangementHoraires",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandeArticles_ComandeId",
                table: "ComandeArticles",
                column: "ComandeId");

            migrationBuilder.CreateIndex(
                name: "IX_comandes_DepartmentId",
                table: "comandes",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_comandes_UserId",
                table: "comandes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conges_IdTypeConge",
                table: "Conges",
                column: "IdTypeConge");

            migrationBuilder.CreateIndex(
                name: "IX_Conges_UserId",
                table: "Conges",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAdjustments_IdTypeShift",
                table: "TimeAdjustments",
                column: "IdTypeShift");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAdjustments_UserId",
                table: "TimeAdjustments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BadgeManquants");

            migrationBuilder.DropTable(
                name: "ChangementHoraires");

            migrationBuilder.DropTable(
                name: "ComandeArticles");

            migrationBuilder.DropTable(
                name: "Componsations");

            migrationBuilder.DropTable(
                name: "Conges");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "comandes");

            migrationBuilder.DropTable(
                name: "TypeConges");

            migrationBuilder.DropTable(
                name: "TimeAdjustments");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "TypeShifts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
