using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fashion.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    parent_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__attribut__3213E83F516EFBCC", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    img = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__brands__3213E83F986773AD", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    img = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categori__3213E83F712DF72D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false),
                    pd = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customer__3213E83F461F94BD", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    price = table.Column<int>(type: "int", nullable: true, defaultValueSql: "('0')"),
                    @abstract = table.Column<string>(name: "abstract", type: "nvarchar(300)", maxLength: 300, nullable: true),
                    description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    img = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true, defaultValueSql: "('1')"),
                    status = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "('0')"),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false),
                    brand_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__products__3213E83F9E40BE96", x => x.id);
                    table.ForeignKey(
                        name: "FK__products__brand___5AEE82B9",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    total_price = table.Column<int>(type: "int", nullable: true),
                    note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    fee = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    order_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orders__3213E83F0DCF5CAB", x => x.id);
                    table.ForeignKey(
                        name: "FK__orders__customer__59FA5E80",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(type: "int", nullable: true, defaultValueSql: "('1')"),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    customer_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__carts__3213E83F3BD2813D", x => x.id);
                    table.ForeignKey(
                        name: "FK__carts__customer___52593CB8",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__carts__product_i__534D60F1",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "lnk_product_attribute",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    attribute_id = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__lnk_prod__3213E83F3943F7FE", x => x.id);
                    table.ForeignKey(
                        name: "FK__lnk_produ__attri__5441852A",
                        column: x => x.attribute_id,
                        principalTable: "attributes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__lnk_produ__produ__5535A963",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "lnk_product_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__lnk_prod__3213E83F566D30A6", x => x.id);
                    table.ForeignKey(
                        name: "FK__lnk_produ__categ__5629CD9C",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__lnk_produ__produ__571DF1D5",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rate = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    deleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "('0')"),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    customer_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reviews__3213E83F0D43DB91", x => x.id);
                    table.ForeignKey(
                        name: "FK__reviews__custome__5BE2A6F2",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__reviews__product__5CD6CB2B",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    order_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_de__3213E83FEB122077", x => x.id);
                    table.ForeignKey(
                        name: "FK__order_det__order__5812160E",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__order_det__produ__59063A47",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__brands__32DD1E4C028755F9",
                table: "brands",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_carts_customer_id",
                table: "carts",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_carts_product_id",
                table: "carts",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "UQ__categori__32DD1E4C7A48EFD9",
                table: "categories",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lnk_product_attribute_attribute_id",
                table: "lnk_product_attribute",
                column: "attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_lnk_product_attribute_product_id",
                table: "lnk_product_attribute",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_lnk_product_category_category_id",
                table: "lnk_product_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_lnk_product_category_product_id",
                table: "lnk_product_category",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_order_id",
                table: "order_detail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_product_id",
                table: "order_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_brand_id",
                table: "products",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "UQ__products__32DD1E4C52B001BA",
                table: "products",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_customer_id",
                table: "reviews",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_product_id",
                table: "reviews",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "lnk_product_attribute");

            migrationBuilder.DropTable(
                name: "lnk_product_category");

            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "brands");
        }
    }
}
