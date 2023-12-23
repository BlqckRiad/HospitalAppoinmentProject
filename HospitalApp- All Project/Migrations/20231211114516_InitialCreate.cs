using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dallar",
                columns: table => new
                {
                    DalId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DalName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dallar", x => x.DalId);
                });

            migrationBuilder.CreateTable(
                name: "Poliklinikler",
                columns: table => new
                {
                    PolId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PolName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliklinikler", x => x.PolId);
                });

            migrationBuilder.CreateTable(
                name: "Doktorlar",
                columns: table => new
                {
                    DoktorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DoktorAd = table.Column<string>(type: "TEXT", nullable: true),
                    DalId = table.Column<int>(type: "INTEGER", nullable: false),
                    PolId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktorlar", x => x.DoktorId);
                    table.ForeignKey(
                        name: "FK_Doktorlar_Dallar_DalId",
                        column: x => x.DalId,
                        principalTable: "Dallar",
                        principalColumn: "DalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doktorlar_Poliklinikler_PolId",
                        column: x => x.PolId,
                        principalTable: "Poliklinikler",
                        principalColumn: "PolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HastaAdi = table.Column<string>(type: "TEXT", nullable: true),
                    DoktorId = table.Column<int>(type: "INTEGER", nullable: false),
                    HastaTC = table.Column<string>(type: "TEXT", nullable: true),
                    HastaTelefonNo = table.Column<string>(type: "TEXT", nullable: true),
                    RandevuTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuId);
                    table.ForeignKey(
                        name: "FK_Randevular_Doktorlar_DoktorId",
                        column: x => x.DoktorId,
                        principalTable: "Doktorlar",
                        principalColumn: "DoktorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_DalId",
                table: "Doktorlar",
                column: "DalId");

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_PolId",
                table: "Doktorlar",
                column: "PolId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_DoktorId",
                table: "Randevular",
                column: "DoktorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Doktorlar");

            migrationBuilder.DropTable(
                name: "Dallar");

            migrationBuilder.DropTable(
                name: "Poliklinikler");
        }
    }
}
