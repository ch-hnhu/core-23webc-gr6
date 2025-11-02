using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core_23webc_gr6.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__23CAF1D824EB68C6", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    contactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contacts__7121FD35D96D4451", x => x.contactId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    tagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tagName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tags__50FC01571FB08230", x => x.tagId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "User"),
                    status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__CB9A1CFF0118B670", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "WebSettings",
                columns: table => new
                {
                    settingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    siteName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    siteDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    logo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    favicon = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    facebook = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    youtube = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    linkedIn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    twitter = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WebSetti__097EE23C6447E7F7", x => x.settingId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    discountPercentage = table.Column<int>(type: "int", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    images = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    views = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__2D10D16A4829F541", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Products_Categories",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "categoryId");
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    billId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    paymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Cash on Delivery"),
                    paymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "unpaid"),
                    shippingMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "express"),
                    discountPercentage = table.Column<int>(type: "int", nullable: false),
                    totalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    deliveryStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "pending"),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bills__6D903F034299EF9F", x => x.billId);
                    table.ForeignKey(
                        name: "FK_Bills_Users",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Carts__415B03B8DE4AE9E8", x => x.cartId);
                    table.ForeignKey(
                        name: "FK_Carts_Users",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false),
                    tagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductT__481F117F54DF7AB0", x => new { x.productId, x.tagId });
                    table.ForeignKey(
                        name: "FK_ProductTags_Product",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTags_Tag",
                        column: x => x.tagId,
                        principalTable: "Tags",
                        principalColumn: "tagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    reviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rating = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)5),
                    status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__2ECD6E046D3EDD5C", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Products",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId");
                    table.ForeignKey(
                        name: "FK_Reviews_Users",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    billDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    unitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    discountPercentage = table.Column<int>(type: "int", nullable: true),
                    subTotal = table.Column<decimal>(type: "decimal(29,2)", nullable: true, computedColumnSql: "([quantity]*[unitPrice])", stored: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__C57B0C9A61F66BFC", x => x.billDetailId);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills",
                        column: x => x.billId,
                        principalTable: "Bills",
                        principalColumn: "billId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_Products",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    cartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CartItem__283983B6285A5BD1", x => x.cartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts",
                        column: x => x.cartId,
                        principalTable: "Carts",
                        principalColumn: "cartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_billId",
                table: "BillDetails",
                column: "billId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_productId",
                table: "BillDetails",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_createdAt",
                table: "Bills",
                column: "createdAt");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_userId",
                table: "Bills",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_cartId",
                table: "CartItems",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_productId",
                table: "CartItems",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_userId",
                table: "Carts",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_categoryName",
                table: "Categories",
                column: "categoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_email",
                table: "Contacts",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryId",
                table: "Products",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_productName",
                table: "Products",
                column: "productName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_tagId",
                table: "ProductTags",
                column: "tagId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_productId",
                table: "Reviews",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_userId",
                table: "Reviews",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_tagName",
                table: "Tags",
                column: "tagName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__AB6E6164B06230CC",
                table: "Users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "WebSettings");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
