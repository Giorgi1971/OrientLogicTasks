using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CredoProject.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "exchangeEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    currencyFrom = table.Column<int>(type: "int", nullable: false),
                    currencyTo = table.Column<int>(type: "int", nullable: false),
                    rate = table.Column<decimal>(type: "decimal(18,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exchangeEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperatorEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatorEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendEmailRequestEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendEmailRequestEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountEntities",
                columns: table => new
                {
                    AccountEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountEntities", x => x.AccountEntityId);
                    table.ForeignKey(
                        name: "FK_AccountEntities_AspNetUsers_CustomerEntityId",
                        column: x => x.CustomerEntityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardEntities",
                columns: table => new
                {
                    CardEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardEntities", x => x.CardEntityId);
                    table.ForeignKey(
                        name: "FK_CardEntities_AccountEntities_AccountEntityId",
                        column: x => x.AccountEntityId,
                        principalTable: "AccountEntities",
                        principalColumn: "AccountEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionEntities",
                columns: table => new
                {
                    AccountFromId = table.Column<int>(type: "int", nullable: false),
                    AccountToId = table.Column<int>(type: "int", nullable: false),
                    TransactionEntityId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyFrom = table.Column<int>(type: "int", nullable: false),
                    CurrencyTo = table.Column<int>(type: "int", nullable: false),
                    AmountTransaction = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    TransType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRate = table.Column<decimal>(type: "decimal(18,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionEntities", x => new { x.AccountFromId, x.AccountToId });
                    table.ForeignKey(
                        name: "FK_TransactionEntities_AccountEntities_AccountFromId",
                        column: x => x.AccountFromId,
                        principalTable: "AccountEntities",
                        principalColumn: "AccountEntityId");
                    table.ForeignKey(
                        name: "FK_TransactionEntities_AccountEntities_AccountToId",
                        column: x => x.AccountToId,
                        principalTable: "AccountEntities",
                        principalColumn: "AccountEntityId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "3eee982c-6bf3-4e2e-be2d-1446a6902462", "api-manager", "API-MANAGER" },
                    { 2, "3635f488-5f30-4df1-9ef7-f9149b5e0ec4", "api-user", "API-USER" },
                    { 3, "fffd850d-2f5d-47b9-a769-92bcfb5d9ba7", "api-operator", "API-OPERATOR" },
                    { 4, "a7547378-55e6-4685-89f2-25cafd1cd324", "api-admin", "API-ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonalNumber", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1971, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "13cde0bc-0081-4ded-9d99-80e1550e6186", "gio5@gmail.com", false, "Gio", "Mas", false, null, "GIO5@GMAIL.COM", null, "AQAAAAEAACcQAAAAENhWYxCxC0g49PmLMo3fAEjhn7vThjLzoKke3HAELY3zvuAXcXdZ3tVxqR2RSqDO3Q==", "01030019697", null, false, null, false, null },
                    { 2, 0, new DateTime(1978, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "00d51627-326e-4aa0-885f-a41d78021474", "nino@gmail.com", false, "Nino", "Chale", false, null, "NINO@GMAIL.COM", null, "AQAAAAEAACcQAAAAEINcu6Uw0/rcV2/W7jcC2Bxtgf3QTghkWfaqcPgkP5P/L1s5HwVJnWiFk/4Tjjtkdw==", "01015003600", null, false, null, false, null },
                    { 3, 0, new DateTime(2017, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "f36bb702-9d9e-4973-82ad-c04eca5c0047", "niko@gmail.com", false, "Niko", "Mas", false, null, "NIKO@GMAIL.COM", null, "AQAAAAEAACcQAAAAEL6sDE43fGJsuK1lxWVT01HVZAQspUClyVlC64cKhwVm7/ymqe45psFrD654SK8Juw==", "01015008765", null, false, null, false, null }
                });

            migrationBuilder.InsertData(
                table: "OperatorEntities",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "0e9cb375-7330-434b-828d-0cd0e8941869", "gio2@gmail.com", false, null, false, null, null, null, null, "AQAAAAEAACcQAAAAEItNVCRQqYROLmsN8SrncVZRD3gr3GEXgCjsh3LenYvTYrdA9HCa1vPFGNNcG5L47Q==", null, false, null, false, "gio2@gmail.com" });

            migrationBuilder.InsertData(
                table: "exchangeEntities",
                columns: new[] { "Id", "currencyFrom", "currencyTo", "date", "rate" },
                values: new object[,]
                {
                    { 1, 0, 0, new DateTime(2023, 3, 19, 0, 19, 27, 788, DateTimeKind.Local).AddTicks(7590), 1m },
                    { 2, 0, 1, new DateTime(2023, 3, 19, 0, 19, 27, 788, DateTimeKind.Local).AddTicks(7620), 0.361m },
                    { 3, 0, 2, new DateTime(2023, 3, 19, 0, 19, 27, 788, DateTimeKind.Local).AddTicks(7620), 0.3636m },
                    { 4, 1, 1, new DateTime(2023, 3, 19, 0, 19, 27, 788, DateTimeKind.Local).AddTicks(7630), 1m },
                    { 5, 1, 0, new DateTime(2023, 3, 19, 0, 19, 27, 788, DateTimeKind.Local).AddTicks(7630), 2.77m },
                    { 6, 1, 2, new DateTime(2023, 3, 19, 0, 19, 27, 788, DateTimeKind.Local).AddTicks(7630), 0.98m },
                    { 7, 2, 2, new DateTime(2023, 3, 19, 0, 19, 27, 788, DateTimeKind.Local).AddTicks(7630), 1m },
                    { 8, 2, 0, new DateTime(2023, 3, 19, 0, 19, 27, 788, DateTimeKind.Local).AddTicks(7630), 2.87m },
                    { 9, 2, 1, new DateTime(2023, 3, 19, 0, 19, 27, 788, DateTimeKind.Local).AddTicks(7640), 1.0071m }
                });

            migrationBuilder.InsertData(
                table: "AccountEntities",
                columns: new[] { "AccountEntityId", "Amount", "CreateAt", "Currency", "CustomerEntityId", "IBAN" },
                values: new object[,]
                {
                    { 1, 5000m, new DateTime(2023, 3, 19, 0, 19, 27, 795, DateTimeKind.Local).AddTicks(6200), 0, 2, "AL35202111090000000001234567" },
                    { 2, 5000m, new DateTime(2023, 3, 19, 0, 19, 27, 795, DateTimeKind.Local).AddTicks(6200), 1, 2, "AD1400080001001234567890" },
                    { 3, 5000m, new DateTime(2023, 3, 19, 0, 19, 27, 795, DateTimeKind.Local).AddTicks(6210), 2, 2, "AT483200000012345864" },
                    { 4, 4000m, new DateTime(2023, 3, 19, 0, 19, 27, 795, DateTimeKind.Local).AddTicks(6210), 0, 3, "AZ77VTBA00000000001234567890" },
                    { 5, 3000m, new DateTime(2023, 3, 19, 0, 19, 27, 795, DateTimeKind.Local).AddTicks(6210), 1, 3, "BH02CITI00001077181611" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 2, 2 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "CardEntities",
                columns: new[] { "CardEntityId", "AccountEntityId", "CVV", "CardNumber", "ExpiredDate", "OwnerLastName", "OwnerName", "PIN", "RegistrationDate", "Status" },
                values: new object[,]
                {
                    { 1, 1, "321", "Card01", "03-2024", "LasName", "Name", "4444", new DateTime(2023, 3, 19, 0, 19, 27, 795, DateTimeKind.Local).AddTicks(6220), 0 },
                    { 2, 2, "321", "Card02", "03-2024", "LasName", "Name", "4444", new DateTime(2023, 3, 19, 0, 19, 27, 795, DateTimeKind.Local).AddTicks(6220), 0 },
                    { 3, 1, "321", "Card03", "03-2022", "LasName", "Name", "4444", new DateTime(2023, 3, 19, 0, 19, 27, 795, DateTimeKind.Local).AddTicks(6230), 0 },
                    { 4, 3, "321", "Card04", "03-2024", "LasName", "Name", "4444", new DateTime(2023, 3, 19, 0, 19, 27, 795, DateTimeKind.Local).AddTicks(6230), 0 },
                    { 5, 4, "321", "Card05", "03-2024", "LasName", "Name", "4444", new DateTime(2023, 3, 19, 0, 19, 27, 795, DateTimeKind.Local).AddTicks(6230), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountEntities_CustomerEntityId",
                table: "AccountEntities",
                column: "CustomerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CardEntities");

            migrationBuilder.DropTable(
                name: "exchangeEntities");

            migrationBuilder.DropTable(
                name: "OperatorEntities");

            migrationBuilder.DropTable(
                name: "SendEmailRequestEntities");

            migrationBuilder.DropTable(
                name: "TransactionEntities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AccountEntities");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
