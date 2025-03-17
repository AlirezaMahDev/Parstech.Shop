using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Persistence.Context;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Categury> Categuries { get; set; }

    public virtual DbSet<CateguryOfUser> CateguryOfUsers { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<CouponPcu> CouponPcus { get; set; }

    public virtual DbSet<CouponType> CouponTypes { get; set; }

    public virtual DbSet<FormCredit> FormCredits { get; set; }

    public virtual DbSet<Irole> Iroles { get; set; }

    public virtual DbSet<IroleClaim> IroleClaims { get; set; }

    public virtual DbSet<Iuser> Iusers { get; set; }

    public virtual DbSet<IuserClaim> IuserClaims { get; set; }

    public virtual DbSet<IuserLogin> IuserLogins { get; set; }

    public virtual DbSet<IuserToken> IuserTokens { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<LogCategury> LogCateguries { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderCoupon> OrderCoupons { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderPay> OrderPays { get; set; }

    public virtual DbSet<OrderShipping> OrderShippings { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<OrdersAction> OrdersActions { get; set; }

    public virtual DbSet<PayStatusType> PayStatusTypes { get; set; }

    public virtual DbSet<PayType> PayTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategury> ProductCateguries { get; set; }

    public virtual DbSet<ProductComment> ProductComments { get; set; }

    public virtual DbSet<ProductGallery> ProductGalleries { get; set; }

    public virtual DbSet<ProductLog> ProductLogs { get; set; }

    public virtual DbSet<ProductLogType> ProductLogTypes { get; set; }

    public virtual DbSet<ProductProperty> ProductProperties { get; set; }

    public virtual DbSet<ProductRating> ProductRatings { get; set; }

    public virtual DbSet<ProductRelated> ProductRelateds { get; set; }

    public virtual DbSet<ProductRepresentation> ProductRepresentations { get; set; }

    public virtual DbSet<ProductStockPrice> ProductStockPrices { get; set; }

    public virtual DbSet<ProductStockPriceSection> ProductStockPriceSections { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<PropertyCategury> PropertyCateguries { get; set; }

    public virtual DbSet<RahkaranOrder> RahkaranOrders { get; set; }

    public virtual DbSet<RahkaranProduct> RahkaranProducts { get; set; }

    public virtual DbSet<RahkaranUser> RahkaranUsers { get; set; }

    public virtual DbSet<Representation> Representations { get; set; }

    public virtual DbSet<RepresentationType> RepresentationTypes { get; set; }

    public virtual DbSet<SecoundPayAfterDargah> SecoundPayAfterDargahs { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<SectionDetail> SectionDetails { get; set; }

    public virtual DbSet<SectionType> SectionTypes { get; set; }

    public virtual DbSet<ShippingType> ShippingTypes { get; set; }

    public virtual DbSet<SiteSetting> SiteSettings { get; set; }

    public virtual DbSet<SocialSetting> SocialSettings { get; set; }

    public virtual DbSet<SpecialProductStockPrice> SpecialProductStockPrices { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketDetail> TicketDetails { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBilling> UserBillings { get; set; }

    public virtual DbSet<UserCategury> UserCateguries { get; set; }

    public virtual DbSet<UserProduct> UserProducts { get; set; }

    public virtual DbSet<UserShipping> UserShippings { get; set; }

    public virtual DbSet<UserStore> UserStores { get; set; }

    public virtual DbSet<VersionSetting> VersionSettings { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    public virtual DbSet<WalletTransaction> WalletTransactions { get; set; }

    public virtual DbSet<WalletType> WalletTypes { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-38OVAOH\\MSSQLSERVER22S;Database=Parstech;Trusted_Connection=True; Encrypt=true;TrustServerCertificate=yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK_Brandses");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandImage).HasMaxLength(200);
            entity.Property(e => e.BrandTitle).HasMaxLength(200);
            entity.Property(e => e.LatinBrandTitle).HasMaxLength(200);
        });

        modelBuilder.Entity<Categury>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK_ProductGroups");

            entity.ToTable("Categury");

            entity.HasIndex(e => e.ParentId, "IX_Categury_ParentId");

            entity.Property(e => e.Alt).HasMaxLength(300);
            entity.Property(e => e.BackImage).HasMaxLength(300);
            entity.Property(e => e.GroupTitle).HasMaxLength(200);
            entity.Property(e => e.Image).HasMaxLength(300);
            entity.Property(e => e.IsParnet).HasColumnName("isParnet");
            entity.Property(e => e.LatinGroupTitle).HasMaxLength(200);

            entity.HasOne(d => d.Parent)
                .WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Categury_Categury");
        });

        modelBuilder.Entity<CateguryOfUser>(entity =>
        {
            entity.ToTable("CateguryOfUser");

            entity.Property(e => e.CateguryName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.ToTable("Coupon");

            entity.HasIndex(e => e.CouponTypeId, "IX_Coupon_CouponTypeId");

            entity.Property(e => e.Categury).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.Products).HasMaxLength(50);
            entity.Property(e => e.Users).HasMaxLength(50);

            entity.HasOne(d => d.CouponType)
                .WithMany(p => p.Coupons)
                .HasForeignKey(d => d.CouponTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Coupon_CouponType");
        });

        modelBuilder.Entity<CouponPcu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CouponProductCatgury");

            entity.ToTable("CouponPCU");

            entity.HasIndex(e => e.CouponId, "IX_CouponPCU_CouponId");

            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Coupon)
                .WithMany(p => p.CouponPcus)
                .HasForeignKey(d => d.CouponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CouponProductCatgury_Coupon");
        });

        modelBuilder.Entity<CouponType>(entity =>
        {
            entity.ToTable("CouponType");

            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<FormCredit>(entity =>
        {
            entity.ToTable("FormCredit");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Family).HasMaxLength(200);
            entity.Property(e => e.InternationalCode).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.PersonalCode).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Irole>(entity =>
        {
            entity.ToTable("IRoles");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
            entity.Property(e => e.PersianName).HasMaxLength(100);
        });

        modelBuilder.Entity<IroleClaim>(entity =>
        {
            entity.ToTable("IRoleClaims");

            entity.HasIndex(e => e.RoleId, "IX_IRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.IroleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<Iuser>(entity =>
        {
            entity.ToTable("IUsers");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "IuserRole",
                    r => r.HasOne<Irole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<Iuser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("IUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_IUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<IuserClaim>(entity =>
        {
            entity.ToTable("IUserClaims");

            entity.HasIndex(e => e.UserId, "IX_IUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.IuserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<IuserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.ToTable("IUserLogins");

            entity.HasIndex(e => e.UserId, "IX_IUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.IuserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<IuserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.ToTable("IUserTokens");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.IuserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");

            entity.HasIndex(e => e.LogCateguryId, "IX_Log_LogCateguryId");

            entity.HasIndex(e => e.UserId, "IX_Log_UserId");

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasOne(d => d.LogCategury)
                .WithMany(p => p.Logs)
                .HasForeignKey(d => d.LogCateguryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Log_LogCategury");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Logs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Log_User");
        });

        modelBuilder.Entity<LogCategury>(entity =>
        {
            entity.ToTable("LogCategury");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.TaxId, "IX_Orders_TaxId");

            entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FactorFile).HasMaxLength(100);
            entity.Property(e => e.IntroCode).HasMaxLength(200);
            entity.Property(e => e.OrderCode)
                .HasMaxLength(30)
                .HasDefaultValue("");

            entity.HasOne(d => d.TaxNavigation)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.TaxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Tax");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_User");
        });

        modelBuilder.Entity<OrderCoupon>(entity =>
        {
            entity.ToTable("OrderCoupon");

            entity.HasIndex(e => e.CouponId, "IX_OrderCoupon_CouponId");

            entity.HasIndex(e => e.OrderId, "IX_OrderCoupon_OrderId");

            entity.HasOne(d => d.Coupon)
                .WithMany(p => p.OrderCoupons)
                .HasForeignKey(d => d.CouponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderCoupon_Coupon");

            entity.HasOne(d => d.Order)
                .WithMany(p => p.OrderCoupons)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderCoupon_Orders");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_WooOrderDetail");

            entity.ToTable("OrderDetail");

            entity.HasIndex(e => e.OrderId, "IX_OrderDetail_OrderId");

            entity.HasIndex(e => e.ProductStockPriceId, "IX_OrderDetail_ProductStockPriceId");

            entity.HasOne(d => d.Order)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Orders");

            entity.HasOne(d => d.ProductStockPrice)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductStockPriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_ProductStockPrice");
        });

        modelBuilder.Entity<OrderPay>(entity =>
        {
            entity.ToTable("OrderPay");

            entity.HasIndex(e => e.OrderId, "IX_OrderPay_OrderId");

            entity.HasIndex(e => e.PayStatusTypeId, "IX_OrderPay_PayStatusTypeId");

            entity.HasIndex(e => e.PayTypeId, "IX_OrderPay_PayTypeId");

            entity.Property(e => e.DepositCode).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(300);

            entity.HasOne(d => d.Order)
                .WithMany(p => p.OrderPays)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderPay_Orders");

            entity.HasOne(d => d.PayStatusType)
                .WithMany(p => p.OrderPays)
                .HasForeignKey(d => d.PayStatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderPay_PayStatusType");

            entity.HasOne(d => d.PayType)
                .WithMany(p => p.OrderPays)
                .HasForeignKey(d => d.PayTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderPay_PayType");
        });

        modelBuilder.Entity<OrderShipping>(entity =>
        {
            entity.ToTable("OrderShipping");

            entity.HasIndex(e => e.OrderId, "IX_OrderShipping_OrderId");

            entity.HasIndex(e => e.ShippingTypeId, "IX_OrderShipping_ShippingTypeId");

            entity.Property(e => e.DeliveryCode).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.FullAddress).HasMaxLength(500);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.PostCode).HasMaxLength(50);
            entity.Property(e => e.StatusName).HasMaxLength(50);
            entity.Property(e => e.UserShippingId).HasMaxLength(50);

            entity.HasOne(d => d.Order)
                .WithMany(p => p.OrderShippings)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderShipping_Orders");

            entity.HasOne(d => d.ShippingType)
                .WithMany(p => p.OrderShippings)
                .HasForeignKey(d => d.ShippingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderShipping_ShippingType");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Osid).HasName("PK_AllOrderStatus");

            entity.ToTable("OrderStatus");

            entity.HasIndex(e => e.OrderId, "IX_OrderStatus_OrderId");

            entity.HasIndex(e => e.StatusId, "IX_OrderStatus_StatusId");

            entity.Property(e => e.Osid).HasColumnName("OSId");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreateBy).HasMaxLength(200);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(150);

            entity.HasOne(d => d.Order)
                .WithMany(p => p.OrderStatuses)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderStatus_Orders");

            entity.HasOne(d => d.Status)
                .WithMany(p => p.OrderStatuses)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderStatus_Status");
        });

        modelBuilder.Entity<OrdersAction>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Caption).HasMaxLength(1);
            entity.Property(e => e.Description).HasMaxLength(1);
            entity.Property(e => e.OrderCode).HasMaxLength(1);
        });

        modelBuilder.Entity<PayStatusType>(entity =>
        {
            entity.ToTable("PayStatusType");

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<PayType>(entity =>
        {
            entity.ToTable("PayType");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.BrandId, "IX_Product_BrandId");

            entity.HasIndex(e => e.ParentId, "IX_Product_ParentId");

            entity.HasIndex(e => e.TaxId, "IX_Product_TaxId");

            entity.HasIndex(e => e.TypeId, "IX_Product_TypeId");

            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Keywords).HasMaxLength(200);
            entity.Property(e => e.LatinName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.ShortDescription).HasMaxLength(160);
            entity.Property(e => e.ShortLink).HasMaxLength(100);
            entity.Property(e => e.TaxCode).HasMaxLength(50);
            entity.Property(e => e.VariationName).HasMaxLength(100);

            entity.HasOne(d => d.Brand)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Brand");

            entity.HasOne(d => d.Parent)
                .WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Product_Product");

            entity.HasOne(d => d.Tax)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.TaxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Tax");

            entity.HasOne(d => d.Type)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductType");
        });

        modelBuilder.Entity<ProductCategury>(entity =>
        {
            entity.ToTable("ProductCategury");

            entity.HasIndex(e => e.CateguryId, "IX_ProductCategury_CateguryId");

            entity.HasIndex(e => e.ProductId, "IX_ProductCategury_ProductId");

            entity.HasOne(d => d.Categury)
                .WithMany(p => p.ProductCateguries)
                .HasForeignKey(d => d.CateguryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductCategury_Categury1");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductCateguries)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductCategury_Product");
        });

        modelBuilder.Entity<ProductComment>(entity =>
        {
            entity.ToTable("ProductComment");

            entity.HasIndex(e => e.ProductId, "IX_ProductComment_ProductId");

            entity.HasIndex(e => e.UserId, "IX_ProductComment_UserId");

            entity.Property(e => e.Comment).HasMaxLength(300);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductComments)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductComment_Product");

            entity.HasOne(d => d.User)
                .WithMany(p => p.ProductComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductComment_User");
        });

        modelBuilder.Entity<ProductGallery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_productGalleries");

            entity.ToTable("ProductGallery");

            entity.HasIndex(e => e.ProductId, "IX_ProductGallery_ProductId");

            entity.Property(e => e.Alt).HasMaxLength(100);

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductGalleries)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductGallery_Product");
        });

        modelBuilder.Entity<ProductLog>(entity =>
        {
            entity.ToTable("ProductLog");

            entity.HasIndex(e => e.ProductLogTypeId, "IX_ProductLog_ProductLogTypeId");

            entity.HasIndex(e => e.ProductStockPriceId, "IX_ProductLog_ProductStockPriceId");

            entity.HasIndex(e => e.UserId, "IX_ProductLog_UserId");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.NewValue).HasMaxLength(500);
            entity.Property(e => e.OldValue).HasMaxLength(500);

            entity.HasOne(d => d.ProductLogType)
                .WithMany(p => p.ProductLogs)
                .HasForeignKey(d => d.ProductLogTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductLog_ProductLogType");

            entity.HasOne(d => d.ProductStockPrice)
                .WithMany(p => p.ProductLogs)
                .HasForeignKey(d => d.ProductStockPriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductLog_ProductStockPrice");

            entity.HasOne(d => d.User)
                .WithMany(p => p.ProductLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductLog_User");
        });

        modelBuilder.Entity<ProductLogType>(entity =>
        {
            entity.ToTable("ProductLogType");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductProperty>(entity =>
        {
            entity.ToTable("ProductProperty");

            entity.HasIndex(e => e.ProductId, "IX_ProductProperty_ProductId");

            entity.HasIndex(e => e.PropertyId, "IX_ProductProperty_PropertyId");

            entity.Property(e => e.Value).HasMaxLength(1500);

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductProperties)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductProperty_Product");

            entity.HasOne(d => d.Property)
                .WithMany(p => p.ProductProperties)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductProperty_Property");
        });

        modelBuilder.Entity<ProductRating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_productRatings");

            entity.ToTable("ProductRating");

            entity.HasIndex(e => e.ProductId, "IX_ProductRating_ProductId");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductRatings)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductRating_Product");
        });

        modelBuilder.Entity<ProductRelated>(entity =>
        {
            entity.ToTable("ProductRelated");

            entity.HasIndex(e => e.FkProductId, "IX_ProductRelated_FkProductId");

            entity.HasIndex(e => e.ProductId, "IX_ProductRelated_ProductId");

            entity.HasOne(d => d.FkProduct)
                .WithMany(p => p.ProductRelatedFkProducts)
                .HasForeignKey(d => d.FkProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductRelated_Product1");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductRelatedProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductRelated_Product");
        });

        modelBuilder.Entity<ProductRepresentation>(entity =>
        {
            entity.ToTable("ProductRepresentation");

            entity.HasIndex(e => e.ProductStockPriceId, "IX_ProductRepresentation_ProductStockPriceId");

            entity.HasIndex(e => e.RepresntationId, "IX_ProductRepresentation_RepresntationId");

            entity.HasIndex(e => e.TypeId, "IX_ProductRepresentation_TypeId");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.UniqeCode).HasMaxLength(50);

            entity.HasOne(d => d.ProductStockPrice)
                .WithMany(p => p.ProductRepresentations)
                .HasForeignKey(d => d.ProductStockPriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductRepresentation_ProductStockPrice");

            entity.HasOne(d => d.Represntation)
                .WithMany(p => p.ProductRepresentations)
                .HasForeignKey(d => d.RepresntationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductRepresentation_Representation");

            entity.HasOne(d => d.Type)
                .WithMany(p => p.ProductRepresentations)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductRepresentation_RepresentationType");
        });

        modelBuilder.Entity<ProductStockPrice>(entity =>
        {
            entity.ToTable("ProductStockPrice");

            entity.HasIndex(e => e.ProductId, "IX_ProductStockPrice_ProductId");

            entity.HasIndex(e => e.RepId, "IX_ProductStockPrice_RepId");

            entity.HasIndex(e => e.StoreId, "IX_ProductStockPrice_StoreId");

            entity.Property(e => e.CateguryOfUserType).HasMaxLength(50);
            entity.Property(e => e.DiscountDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductStockPrices)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStockPrice_Product");

            entity.HasOne(d => d.Rep)
                .WithMany(p => p.ProductStockPrices)
                .HasForeignKey(d => d.RepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStockPrice_Representation");

            entity.HasOne(d => d.Store)
                .WithMany(p => p.ProductStockPrices)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStockPrice_UserStore");
        });

        modelBuilder.Entity<ProductStockPriceSection>(entity =>
        {
            entity.ToTable("ProductStockPriceSection");

            entity.HasOne(d => d.ProductStockPrice)
                .WithMany(p => p.ProductStockPriceSections)
                .HasForeignKey(d => d.ProductStockPriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStockPriceSection_ProductStockPrice1");

            entity.HasOne(d => d.Section)
                .WithMany(p => p.ProductStockPriceSections)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStockPriceSection_Section");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.ToTable("ProductType");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.ToTable("Property");

            entity.HasIndex(e => e.CateguryId, "IX_Property_CateguryId");

            entity.HasIndex(e => e.PropertyCateguryId, "IX_Property_PropertyCateguryId");

            entity.Property(e => e.Caption).HasMaxLength(1500);

            entity.HasOne(d => d.Categury)
                .WithMany(p => p.Properties)
                .HasForeignKey(d => d.CateguryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Property_Categury");

            entity.HasOne(d => d.PropertyCategury)
                .WithMany(p => p.Properties)
                .HasForeignKey(d => d.PropertyCateguryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Property_PropertyCategury");
        });

        modelBuilder.Entity<PropertyCategury>(entity =>
        {
            entity.ToTable("PropertyCategury");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<RahkaranOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RahkaranOrder");

            entity.Property(e => e.RahakaranFactorNumber).HasMaxLength(50);
            entity.Property(e => e.RahakaranFactorSerial).HasMaxLength(50);
            entity.Property(e => e.RahkaranPishNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<RahkaranProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RahkaranProduct");

            entity.Property(e => e.RahkaranProductId).HasMaxLength(50);
        });

        modelBuilder.Entity<RahkaranUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RahkaranUser");

            entity.Property(e => e.RahkaranUserId).HasMaxLength(50);
        });

        modelBuilder.Entity<Representation>(entity =>
        {
            entity.ToTable("Representation");

            entity.HasIndex(e => e.StateId, "IX_Representation_StateId");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.State)
                .WithMany(p => p.Representations)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_Representation_States");
        });

        modelBuilder.Entity<RepresentationType>(entity =>
        {
            entity.ToTable("RepresentationType");

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<SecoundPayAfterDargah>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SecoundPayAfterDargah");

            entity.Property(e => e.Month).HasColumnName("month");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.TransactionId).HasColumnName("transactionId");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.ToTable("Section");

            entity.HasIndex(e => e.SectionTypeId, "IX_Section_SectionTypeId");

            entity.Property(e => e.SectionName).HasMaxLength(150);

            entity.HasOne(d => d.SectionType)
                .WithMany(p => p.Sections)
                .HasForeignKey(d => d.SectionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Section_SectionType");
        });

        modelBuilder.Entity<SectionDetail>(entity =>
        {
            entity.ToTable("SectionDetail");

            entity.HasIndex(e => e.SectionId, "IX_SectionDetail_SectionId");

            entity.HasIndex(e => e.SectionTypeId, "IX_SectionDetail_SectionTypeId");

            entity.Property(e => e.Alt).HasMaxLength(200);
            entity.Property(e => e.BackgroundColor).HasMaxLength(50);
            entity.Property(e => e.BackgroundImage).HasMaxLength(300);
            entity.Property(e => e.Caption).HasMaxLength(200);
            entity.Property(e => e.Image).HasMaxLength(300);
            entity.Property(e => e.Link).HasMaxLength(200);
            entity.Property(e => e.ResponsiveSize).HasMaxLength(50);
            entity.Property(e => e.SlideNavName).HasMaxLength(200);
            entity.Property(e => e.SubCaption).HasMaxLength(200);

            entity.HasOne(d => d.Section)
                .WithMany(p => p.SectionDetails)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SectionDetail_Section");

            entity.HasOne(d => d.SectionType)
                .WithMany(p => p.SectionDetails)
                .HasForeignKey(d => d.SectionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SectionDetail_SectionType");
        });

        modelBuilder.Entity<SectionType>(entity =>
        {
            entity.ToTable("SectionType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<ShippingType>(entity =>
        {
            entity.ToTable("ShippingType");

            entity.Property(e => e.Type).HasMaxLength(150);
        });

        modelBuilder.Entity<SiteSetting>(entity =>
        {
            entity.ToTable("SiteSetting");

            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Canonical).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Enamad).HasMaxLength(500);
            entity.Property(e => e.EtaemadElectronic).HasMaxLength(500);
            entity.Property(e => e.Keywords).HasMaxLength(200);
            entity.Property(e => e.Logo).HasMaxLength(200);
            entity.Property(e => e.LogoAlt).HasMaxLength(100);
            entity.Property(e => e.OgDescription)
                .HasMaxLength(250)
                .HasColumnName("Og_Description");
            entity.Property(e => e.OgImage)
                .HasMaxLength(100)
                .HasColumnName("Og_Image");
            entity.Property(e => e.OgSiteName)
                .HasMaxLength(100)
                .HasColumnName("Og_SiteName");
            entity.Property(e => e.OgTitle)
                .HasMaxLength(100)
                .HasColumnName("Og_Title");
            entity.Property(e => e.OgType)
                .HasMaxLength(100)
                .HasColumnName("Og_Type");
            entity.Property(e => e.OgUrl)
                .HasMaxLength(100)
                .HasColumnName("Og_Url");
            entity.Property(e => e.Owner).HasMaxLength(100);
            entity.Property(e => e.RobotsFollow)
                .HasMaxLength(100)
                .HasColumnName("Robots_Follow");
            entity.Property(e => e.RobotsIndex)
                .HasMaxLength(100)
                .HasColumnName("Robots_Index");
            entity.Property(e => e.SiteName).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(65);
        });

        modelBuilder.Entity<SocialSetting>(entity =>
        {
            entity.ToTable("SocialSetting");

            entity.HasIndex(e => e.SiteSettingId, "IX_SocialSetting_SiteSettingId");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .HasColumnName("image");
            entity.Property(e => e.Site)
                .HasMaxLength(100)
                .HasColumnName("site");
            entity.Property(e => e.SocialName).HasMaxLength(100);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.SiteSetting)
                .WithMany(p => p.SocialSettings)
                .HasForeignKey(d => d.SiteSettingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SocialSetting_SiteSetting");
        });

        modelBuilder.Entity<SpecialProductStockPrice>(entity =>
        {
            entity.ToTable("SpecialProductStockPrice");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.StateTitle).HasMaxLength(30);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.StatusLatinName).HasMaxLength(50);
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.ToTable("Tax");

            entity.Property(e => e.TaxName).HasMaxLength(100);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasIndex(e => e.StatusId, "IX_Tickets_StatusId");

            entity.HasIndex(e => e.UserId, "IX_Tickets_UserId");

            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");
            entity.Property(e => e.TicketCaption).HasDefaultValue("");

            entity.HasOne(d => d.Status)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_TicketStatuses");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_User");
        });

        modelBuilder.Entity<TicketDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId);

            entity.HasIndex(e => e.TicketId, "IX_TicketDetails_TicketId");

            entity.HasIndex(e => e.TypeId, "IX_TicketDetails_TypeId");

            entity.Property(e => e.TicketFile).HasMaxLength(200);
            entity.Property(e => e.TicketText).HasMaxLength(2000);

            entity.HasOne(d => d.Ticket)
                .WithMany(p => p.TicketDetails)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketDetails_Tickets");

            entity.HasOne(d => d.Type)
                .WithMany(p => p.TicketDetails)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketDetails_TicketTypes");
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasKey(e => e.TypeId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users");

            entity.ToTable("User");

            entity.Property(e => e.ActiveCode).HasMaxLength(50);
            entity.Property(e => e.Avatar).HasMaxLength(200);
            entity.Property(e => e.UserId).HasMaxLength(450);
            entity.Property(e => e.UserName).HasMaxLength(200);
        });

        modelBuilder.Entity<UserBilling>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_WooOrderBilling");

            entity.ToTable("UserBilling");

            entity.HasIndex(e => e.UserId, "IX_UserBilling_UserId");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Company).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.EconomicCode).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.NationalCode).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.PostCode).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(50);

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserBillings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBilling_User");
        });

        modelBuilder.Entity<UserCategury>(entity =>
        {
            entity.ToTable("UserCategury");

            entity.HasOne(d => d.Categury)
                .WithMany(p => p.UserCateguries)
                .HasForeignKey(d => d.CateguryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCategury_CateguryOfUser");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserCateguries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCategury_User");
        });

        modelBuilder.Entity<UserProduct>(entity =>
        {
            entity.ToTable("UserProduct");

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<UserShipping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_WooOrderShipping");

            entity.ToTable("UserShipping");

            entity.HasIndex(e => e.UserId, "IX_UserShipping_UserId");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(500);
            entity.Property(e => e.Country).HasMaxLength(500);
            entity.Property(e => e.FirstName).HasMaxLength(500);
            entity.Property(e => e.LastName).HasMaxLength(500);
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(500);
            entity.Property(e => e.PostCode).HasMaxLength(500);
            entity.Property(e => e.State).HasMaxLength(500);

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserShippings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserShipping_User");
        });

        modelBuilder.Entity<UserStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_WooStore");

            entity.ToTable("UserStore");

            entity.HasIndex(e => e.RepId, "IX_UserStore_RepId");

            entity.HasIndex(e => e.UserId, "IX_UserStore_UserId");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(200);
            entity.Property(e => e.Country).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(200);
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.LatinStoreName).HasMaxLength(200);
            entity.Property(e => e.Mobile).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(200);
            entity.Property(e => e.PostCode).HasMaxLength(200);
            entity.Property(e => e.State).HasMaxLength(200);
            entity.Property(e => e.StoreName).HasMaxLength(200);

            entity.HasOne(d => d.Rep)
                .WithMany(p => p.UserStores)
                .HasForeignKey(d => d.RepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserStore_Representation");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserStores)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserStore_User1");
        });

        modelBuilder.Entity<VersionSetting>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("VersionSetting");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Version).HasMaxLength(50);
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Wallets_UserId");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Wallets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wallets_User");
        });

        modelBuilder.Entity<WalletTransaction>(entity =>
        {
            entity.ToTable("WalletTransaction");

            entity.HasIndex(e => e.TypeId, "IX_WalletTransaction_TypeId");

            entity.HasIndex(e => e.WalletId, "IX_WalletTransaction_WalletId");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(500);
            entity.Property(e => e.TrackingCode).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(300);

            entity.HasOne(d => d.TypeNavigation)
                .WithMany(p => p.WalletTransactions)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WalletTransaction_WalletTypes");

            entity.HasOne(d => d.Wallet)
                .WithMany(p => p.WalletTransactions)
                .HasForeignKey(d => d.WalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WalletTransaction_Wallets");
        });

        modelBuilder.Entity<WalletType>(entity =>
        {
            entity.HasKey(e => e.TypeId);

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.TypeTitle).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}