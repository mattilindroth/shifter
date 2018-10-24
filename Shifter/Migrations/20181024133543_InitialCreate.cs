using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shifter.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessRightsGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRightsGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workshift",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshift", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkshiftPartTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsWork = table.Column<bool>(nullable: false),
                    DefaultDurationMinutes = table.Column<int>(nullable: false),
                    DefaultStartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshiftPartTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkshiftTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshiftTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessRights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Access = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AccessRightsGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRights_AccessRightsGroup_AccessRightsGroupId",
                        column: x => x.AccessRightsGroupId,
                        principalTable: "AccessRightsGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false),
                    AccessRightsGroupId = table.Column<int>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    PasswordSatl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_AccessRightsGroup_AccessRightsGroupId",
                        column: x => x.AccessRightsGroupId,
                        principalTable: "AccessRightsGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkshiftPart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TemplateId = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    DurationMinutes = table.Column<int>(nullable: false),
                    WorkshiftTemplateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshiftPart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkshiftPart_WorkshiftTemplate_WorkshiftTemplateId",
                        column: x => x.WorkshiftTemplateId,
                        principalTable: "WorkshiftTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRights_AccessRightsGroupId",
                table: "AccessRights",
                column: "AccessRightsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccessRightsGroupId",
                table: "Users",
                column: "AccessRightsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshiftPart_WorkshiftTemplateId",
                table: "WorkshiftPart",
                column: "WorkshiftTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRights");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Workshift");

            migrationBuilder.DropTable(
                name: "WorkshiftPart");

            migrationBuilder.DropTable(
                name: "WorkshiftPartTemplate");

            migrationBuilder.DropTable(
                name: "AccessRightsGroup");

            migrationBuilder.DropTable(
                name: "WorkshiftTemplate");
        }
    }
}
