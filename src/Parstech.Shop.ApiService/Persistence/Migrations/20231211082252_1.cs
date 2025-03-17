using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LatinBrandTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BrandImage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ChangeByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brandses", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Categury",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    LatinGroupTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ChangeByUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastChangeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isParnet = table.Column<bool>(type: "bit", nullable: false),
                    Show = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    BackImage = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Alt = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Row = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Categury_Categury",
                        column: x => x.ParentId,
                        principalTable: "Categury",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateTable(
                name: "CouponType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormCredit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PersonalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InternationalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequestPrice = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormCredit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersianName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_IUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogCategury",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogCategury", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdersActions",
                columns: table => new
                {
                    OrderCode = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "PayStatusType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayStatusType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductLogType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyCategury",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyCategury", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepresentationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LogoAlt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Canonical = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Og_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Og_Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Og_Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Og_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Og_Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Og_SiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Robots_Index = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Robots_Follow = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Enamad = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EtaemadElectronic = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: true),
                    longitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusLatinName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Olaviyat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendSms = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    ActiveCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VersionSetting",
                columns: table => new
                {
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "WalletTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTypes", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponTypeId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    JustNewUser = table.Column<bool>(type: "bit", nullable: false),
                    MinPrice = table.Column<long>(type: "bigint", nullable: true),
                    MaxPrice = table.Column<long>(type: "bigint", nullable: true),
                    TwoUseSameTime = table.Column<bool>(type: "bit", nullable: false),
                    LimitUse = table.Column<int>(type: "int", nullable: true),
                    LimitEachUser = table.Column<int>(type: "int", nullable: true),
                    Categury = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Products = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Users = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Persent = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coupon_CouponType",
                        column: x => x.CouponTypeId,
                        principalTable: "CouponType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IRoleClaims_IRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IUserClaims_IUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IUserLogins_IUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IUserRoles_IRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IUserRoles_IUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_IUserTokens_IUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Caption = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    PropertyCateguryId = table.Column<int>(type: "int", nullable: false),
                    CateguryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Property_Categury",
                        column: x => x.CateguryId,
                        principalTable: "Categury",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_Property_PropertyCategury",
                        column: x => x.PropertyCateguryId,
                        principalTable: "PropertyCategury",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    CateguryId = table.Column<int>(type: "int", nullable: true),
                    SectionTypeId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section_SectionType",
                        column: x => x.SectionTypeId,
                        principalTable: "SectionType",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SocialSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteSettingId = table.Column<int>(type: "int", nullable: false),
                    SocialName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    site = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialSetting_SiteSetting",
                        column: x => x.SiteSettingId,
                        principalTable: "SiteSetting",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Representation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Representation_States",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LatinName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TaxCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    ShortLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    VariationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    TaxId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Visit = table.Column<int>(type: "int", nullable: false),
                    SingleSale = table.Column<bool>(type: "bit", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId");
                    table.ForeignKey(
                        name: "FK_Product_Product",
                        column: x => x.ParentId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_ProductType",
                        column: x => x.TypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Tax",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreateDate = table.Column<int>(type: "int", nullable: false),
                    LogCateguryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_LogCategury",
                        column: x => x.LogCateguryId,
                        principalTable: "LogCategury",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Log_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    OrderCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValueSql: "(N'')"),
                    OrderSum = table.Column<long>(type: "bigint", nullable: false),
                    Tax = table.Column<long>(type: "bigint", nullable: false),
                    Discount = table.Column<long>(type: "bigint", nullable: false),
                    Shipping = table.Column<long>(type: "bigint", nullable: false),
                    Total = table.Column<long>(type: "bigint", nullable: false),
                    IsFinaly = table.Column<bool>(type: "bit", nullable: false),
                    IntroCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IntroCoin = table.Column<int>(type: "int", nullable: false),
                    ConfirmPayment = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    FactorFile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TaxId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Tax",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    departmentId = table.Column<int>(type: "int", nullable: false),
                    TicketCaption = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(N'')"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketStatuses",
                        column: x => x.StatusId,
                        principalTable: "TicketStatuses",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK_Tickets_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserBilling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Company = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EconomicCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WooOrderBilling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBilling_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserShipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    State = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WooOrderShipping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserShipping_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Fecilities = table.Column<long>(type: "bigint", nullable: false),
                    OrgCredit = table.Column<long>(type: "bigint", nullable: false),
                    Coin = table.Column<int>(type: "int", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_Wallets_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CouponPCU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    YesOrNo = table.Column<bool>(type: "bit", nullable: false),
                    FkId = table.Column<int>(type: "int", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponProductCatgury", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponProductCatgury_Coupon",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SectionDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Alt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Link = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SubCaption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SectionTypeId = table.Column<int>(type: "int", nullable: false),
                    BackgroundImage = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    BackgroundColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SlideNavName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ResponsiveSize = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ColSpace = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionDetail_Section",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SectionDetail_SectionType",
                        column: x => x.SectionTypeId,
                        principalTable: "SectionType",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LatinStoreName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    State = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RepId = table.Column<int>(type: "int", nullable: false),
                    PersentOfSale = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WooStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStore_Representation",
                        column: x => x.RepId,
                        principalTable: "Representation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserStore_User1",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductCategury",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CateguryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategury", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategury_Categury1",
                        column: x => x.CateguryId,
                        principalTable: "Categury",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_ProductCategury_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComment_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductComment_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGallery_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductProperty_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductProperty_Property",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductRating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRating_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductRelated",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    FkProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRelated", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRelated_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductRelated_Product1",
                        column: x => x.FkProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderCoupon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: false),
                    DiscountPrice = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCoupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderCoupon_Coupon",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderCoupon_Orders",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateTable(
                name: "OrderPay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PayTypeId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PayStatusTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPay_Orders",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderPay_PayStatusType",
                        column: x => x.PayStatusTypeId,
                        principalTable: "PayStatusType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderPay_PayType",
                        column: x => x.PayTypeId,
                        principalTable: "PayType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderShipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ShippingTypeId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FullAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserShippingId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShipping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderShipping_Orders",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderShipping_ShippingType",
                        column: x => x.ShippingTypeId,
                        principalTable: "ShippingType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    OSId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllOrderStatus", x => x.OSId);
                    table.ForeignKey(
                        name: "FK_OrderStatus_Orders",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderStatus_Status",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketDetails",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    TicketText = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    TicketFile = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDetails", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_TicketDetails_TicketTypes",
                        column: x => x.TypeId,
                        principalTable: "TicketTypes",
                        principalColumn: "TypeId");
                    table.ForeignKey(
                        name: "FK_TicketDetails_Tickets",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId");
                });

            migrationBuilder.CreateTable(
                name: "WalletTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    TrackingCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Persent = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Start = table.Column<bool>(type: "bit", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    ParentFecilitiesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransaction_WalletTypes",
                        column: x => x.TypeId,
                        principalTable: "WalletTypes",
                        principalColumn: "TypeId");
                    table.ForeignKey(
                        name: "FK_WalletTransaction_Wallets",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId");
                });

            migrationBuilder.CreateTable(
                name: "ProductStockPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    SalePrice = table.Column<long>(type: "bigint", nullable: false),
                    DiscountPrice = table.Column<long>(type: "bigint", nullable: false),
                    BasePrice = table.Column<long>(type: "bigint", nullable: false),
                    StockStatus = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MaximumSaleInOrder = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    RepId = table.Column<int>(type: "int", nullable: false),
                    TaxId = table.Column<int>(type: "int", nullable: false),
                    QuantityPerBundle = table.Column<int>(type: "int", nullable: true),
                    DiscountDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStockPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductStockPrice_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductStockPrice_Representation",
                        column: x => x.RepId,
                        principalTable: "Representation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductStockPrice_UserStore",
                        column: x => x.StoreId,
                        principalTable: "UserStore",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Tax = table.Column<long>(type: "bigint", nullable: false),
                    DetailSum = table.Column<long>(type: "bigint", nullable: false),
                    Discount = table.Column<long>(type: "bigint", nullable: false),
                    Total = table.Column<long>(type: "bigint", nullable: false),
                    ProductStockPriceId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WooOrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Orders",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderDetail_ProductStockPrice",
                        column: x => x.ProductStockPriceId,
                        principalTable: "ProductStockPrice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductLogTypeId = table.Column<int>(type: "int", nullable: false),
                    ProductStockPriceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLog_ProductLogType",
                        column: x => x.ProductLogTypeId,
                        principalTable: "ProductLogType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductLog_ProductStockPrice",
                        column: x => x.ProductStockPriceId,
                        principalTable: "ProductStockPrice",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductLog_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductRepresentation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UniqeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    RepresntationId = table.Column<int>(type: "int", nullable: false),
                    ProductStockPriceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRepresentation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRepresentation_ProductStockPrice",
                        column: x => x.ProductStockPriceId,
                        principalTable: "ProductStockPrice",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductRepresentation_Representation",
                        column: x => x.RepresntationId,
                        principalTable: "Representation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductRepresentation_RepresentationType",
                        column: x => x.TypeId,
                        principalTable: "RepresentationType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categury_ParentId",
                table: "Categury",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_CouponTypeId",
                table: "Coupon",
                column: "CouponTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponPCU_CouponId",
                table: "CouponPCU",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_IRoleClaims_RoleId",
                table: "IRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "IRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_IUserClaims_UserId",
                table: "IUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IUserLogins_UserId",
                table: "IUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IUserRoles_RoleId",
                table: "IUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "IUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "IUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Log_LogCateguryId",
                table: "Log",
                column: "LogCateguryId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_UserId",
                table: "Log",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCoupon_CouponId",
                table: "OrderCoupon",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCoupon_OrderId",
                table: "OrderCoupon",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductStockPriceId",
                table: "OrderDetail",
                column: "ProductStockPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPay_OrderId",
                table: "OrderPay",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPay_PayStatusTypeId",
                table: "OrderPay",
                column: "PayStatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPay_PayTypeId",
                table: "OrderPay",
                column: "PayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TaxId",
                table: "Orders",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShipping_OrderId",
                table: "OrderShipping",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShipping_ShippingTypeId",
                table: "OrderShipping",
                column: "ShippingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatus_OrderId",
                table: "OrderStatus",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatus_StatusId",
                table: "OrderStatus",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ParentId",
                table: "Product",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TaxId",
                table: "Product",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TypeId",
                table: "Product",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategury_CateguryId",
                table: "ProductCategury",
                column: "CateguryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategury_ProductId",
                table: "ProductCategury",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComment_ProductId",
                table: "ProductComment",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComment_UserId",
                table: "ProductComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGallery_ProductId",
                table: "ProductGallery",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLog_ProductLogTypeId",
                table: "ProductLog",
                column: "ProductLogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLog_ProductStockPriceId",
                table: "ProductLog",
                column: "ProductStockPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLog_UserId",
                table: "ProductLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperty_ProductId",
                table: "ProductProperty",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProperty_PropertyId",
                table: "ProductProperty",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRating_ProductId",
                table: "ProductRating",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelated_FkProductId",
                table: "ProductRelated",
                column: "FkProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelated_ProductId",
                table: "ProductRelated",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRepresentation_ProductStockPriceId",
                table: "ProductRepresentation",
                column: "ProductStockPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRepresentation_RepresntationId",
                table: "ProductRepresentation",
                column: "RepresntationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRepresentation_TypeId",
                table: "ProductRepresentation",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStockPrice_ProductId",
                table: "ProductStockPrice",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStockPrice_RepId",
                table: "ProductStockPrice",
                column: "RepId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStockPrice_StoreId",
                table: "ProductStockPrice",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_CateguryId",
                table: "Property",
                column: "CateguryId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_PropertyCateguryId",
                table: "Property",
                column: "PropertyCateguryId");

            migrationBuilder.CreateIndex(
                name: "IX_Representation_StateId",
                table: "Representation",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_SectionTypeId",
                table: "Section",
                column: "SectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionDetail_SectionId",
                table: "SectionDetail",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionDetail_SectionTypeId",
                table: "SectionDetail",
                column: "SectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialSetting_SiteSettingId",
                table: "SocialSetting",
                column: "SiteSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_TicketId",
                table: "TicketDetails",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_TypeId",
                table: "TicketDetails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_StatusId",
                table: "Tickets",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBilling_UserId",
                table: "UserBilling",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserShipping_UserId",
                table: "UserShipping",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStore_RepId",
                table: "UserStore",
                column: "RepId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStore_UserId",
                table: "UserStore",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransaction_TypeId",
                table: "WalletTransaction",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransaction_WalletId",
                table: "WalletTransaction",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponPCU");

            migrationBuilder.DropTable(
                name: "FormCredit");

            migrationBuilder.DropTable(
                name: "IRoleClaims");

            migrationBuilder.DropTable(
                name: "IUserClaims");

            migrationBuilder.DropTable(
                name: "IUserLogins");

            migrationBuilder.DropTable(
                name: "IUserRoles");

            migrationBuilder.DropTable(
                name: "IUserTokens");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "OrderCoupon");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "OrderPay");

            migrationBuilder.DropTable(
                name: "OrdersActions");

            migrationBuilder.DropTable(
                name: "OrderShipping");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "ProductCategury");

            migrationBuilder.DropTable(
                name: "ProductComment");

            migrationBuilder.DropTable(
                name: "ProductGallery");

            migrationBuilder.DropTable(
                name: "ProductLog");

            migrationBuilder.DropTable(
                name: "ProductProperty");

            migrationBuilder.DropTable(
                name: "ProductRating");

            migrationBuilder.DropTable(
                name: "ProductRelated");

            migrationBuilder.DropTable(
                name: "ProductRepresentation");

            migrationBuilder.DropTable(
                name: "SectionDetail");

            migrationBuilder.DropTable(
                name: "SocialSetting");

            migrationBuilder.DropTable(
                name: "TicketDetails");

            migrationBuilder.DropTable(
                name: "UserBilling");

            migrationBuilder.DropTable(
                name: "UserProduct");

            migrationBuilder.DropTable(
                name: "UserShipping");

            migrationBuilder.DropTable(
                name: "VersionSetting");

            migrationBuilder.DropTable(
                name: "WalletTransaction");

            migrationBuilder.DropTable(
                name: "IRoles");

            migrationBuilder.DropTable(
                name: "IUsers");

            migrationBuilder.DropTable(
                name: "LogCategury");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "PayStatusType");

            migrationBuilder.DropTable(
                name: "PayType");

            migrationBuilder.DropTable(
                name: "ShippingType");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "ProductLogType");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "ProductStockPrice");

            migrationBuilder.DropTable(
                name: "RepresentationType");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "SiteSetting");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "WalletTypes");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "CouponType");

            migrationBuilder.DropTable(
                name: "Categury");

            migrationBuilder.DropTable(
                name: "PropertyCategury");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "UserStore");

            migrationBuilder.DropTable(
                name: "SectionType");

            migrationBuilder.DropTable(
                name: "TicketStatuses");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Representation");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
