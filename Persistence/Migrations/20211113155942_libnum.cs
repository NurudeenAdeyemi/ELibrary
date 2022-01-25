using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class libnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LibrarianIdentificationNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LibraryIdentificationNumber",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "LibraryNumber",
                table: "Users",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VerificationStatus",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookTransactionId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTransactionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BookTransactionId = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTransactionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookTransactionItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTransactionItems_BookTransactions_BookTransactionId",
                        column: x => x.BookTransactionId,
                        principalTable: "BookTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTransactionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTransactionId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTransactionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookTransactionLogs_BookTransactions_BookTransactionId",
                        column: x => x.BookTransactionId,
                        principalTable: "BookTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LibraryNumber",
                table: "Users",
                column: "LibraryNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookTransactionId",
                table: "Books",
                column: "BookTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTransactionItems_BookId",
                table: "BookTransactionItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTransactionItems_BookTransactionId",
                table: "BookTransactionItems",
                column: "BookTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTransactionLogs_BookTransactionId",
                table: "BookTransactionLogs",
                column: "BookTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTransactions_UserId",
                table: "BookTransactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookTransactions_BookTransactionId",
                table: "Books",
                column: "BookTransactionId",
                principalTable: "BookTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookTransactions_BookTransactionId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookTransactionItems");

            migrationBuilder.DropTable(
                name: "BookTransactionLogs");

            migrationBuilder.DropTable(
                name: "BookTransactions");

            migrationBuilder.DropIndex(
                name: "IX_Users_LibraryNumber",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookTransactionId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LibraryNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BookTransactionId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "LibrarianIdentificationNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibraryIdentificationNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
