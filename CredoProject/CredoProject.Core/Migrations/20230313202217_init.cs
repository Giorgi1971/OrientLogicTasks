using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CredoProject.Core.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatorEntities",
                columns: table => new
                {
                    OperatorEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorEntities", x => x.OperatorEntityId);
                });

            migrationBuilder.CreateTable(
                name: "AccountEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CustomerEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountEntities_CustomerEntities_CustomerEntityId",
                        column: x => x.CustomerEntityId,
                        principalTable: "CustomerEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    AccountEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardEntities_AccountEntities_AccountEntityId",
                        column: x => x.AccountEntityId,
                        principalTable: "AccountEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionEntities",
                columns: table => new
                {
                    AccountFromId = table.Column<int>(type: "int", nullable: false),
                    AccountToId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    AmountTransaction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionEntities", x => new { x.AccountFromId, x.AccountToId });
                    table.ForeignKey(
                        name: "FK_TransactionEntities_AccountEntities_AccountFromId",
                        column: x => x.AccountFromId,
                        principalTable: "AccountEntities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionEntities_AccountEntities_AccountToId",
                        column: x => x.AccountToId,
                        principalTable: "AccountEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountEntities_CustomerEntityId",
                table: "AccountEntities",
                column: "CustomerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CardEntities_AccountEntityId",
                table: "CardEntities",
                column: "AccountEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntities_AccountToId",
                table: "TransactionEntities",
                column: "AccountToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardEntities");

            migrationBuilder.DropTable(
                name: "OperatorEntities");

            migrationBuilder.DropTable(
                name: "TransactionEntities");

            migrationBuilder.DropTable(
                name: "AccountEntities");

            migrationBuilder.DropTable(
                name: "CustomerEntities");
        }
    }
}
