using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ch.gibz.m151.projekt.Data.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Beitrags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErstelltAm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Inhalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beitrags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beitrags_AspNetUsers_AutorId",
                        column: x => x.AutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dateis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dateis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeitragLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IstDislike = table.Column<int>(type: "int", nullable: true),
                    BeitragId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeitragLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeitragLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BeitragLikes_Beitrags_BeitragId",
                        column: x => x.BeitragId,
                        principalTable: "Beitrags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kommentars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inhalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BeitragId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kommentars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kommentars_AspNetUsers_AutorId",
                        column: x => x.AutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kommentars_Beitrags_BeitragId",
                        column: x => x.BeitragId,
                        principalTable: "Beitrags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeitragDateis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeitragId = table.Column<int>(type: "int", nullable: true),
                    DateiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeitragDateis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeitragDateis_Beitrags_BeitragId",
                        column: x => x.BeitragId,
                        principalTable: "Beitrags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BeitragDateis_Dateis_DateiId",
                        column: x => x.DateiId,
                        principalTable: "Dateis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KommentarLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    KommentarId = table.Column<int>(type: "int", nullable: false),
                    IstDislike = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KommentarLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KommentarLikes_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KommentarLikes_Kommentars_KommentarId",
                        column: x => x.KommentarId,
                        principalTable: "Kommentars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeitragDateis_BeitragId",
                table: "BeitragDateis",
                column: "BeitragId");

            migrationBuilder.CreateIndex(
                name: "IX_BeitragDateis_DateiId",
                table: "BeitragDateis",
                column: "DateiId");

            migrationBuilder.CreateIndex(
                name: "IX_BeitragLikes_BeitragId",
                table: "BeitragLikes",
                column: "BeitragId");

            migrationBuilder.CreateIndex(
                name: "IX_BeitragLikes_UserId",
                table: "BeitragLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Beitrags_AutorId",
                table: "Beitrags",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_KommentarLikes_KommentarId",
                table: "KommentarLikes",
                column: "KommentarId");

            migrationBuilder.CreateIndex(
                name: "IX_KommentarLikes_UserId1",
                table: "KommentarLikes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Kommentars_AutorId",
                table: "Kommentars",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Kommentars_BeitragId",
                table: "Kommentars",
                column: "BeitragId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeitragDateis");

            migrationBuilder.DropTable(
                name: "BeitragLikes");

            migrationBuilder.DropTable(
                name: "KommentarLikes");

            migrationBuilder.DropTable(
                name: "Dateis");

            migrationBuilder.DropTable(
                name: "Kommentars");

            migrationBuilder.DropTable(
                name: "Beitrags");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");
        }
    }
}
