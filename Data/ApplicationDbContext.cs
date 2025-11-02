using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using core_23webc_gr6.Models.Entities;

namespace core_23webc_gr6.Data;

public partial class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Bill> Bills { get; set; }

	public virtual DbSet<BillDetail> BillDetails { get; set; }

	public virtual DbSet<Cart> Carts { get; set; }

	public virtual DbSet<CartItem> CartItems { get; set; }

	public virtual DbSet<Category> Categories { get; set; }

	public virtual DbSet<Contact> Contacts { get; set; }

	public virtual DbSet<Product> Products { get; set; }

	public virtual DbSet<Review> Reviews { get; set; }

	public virtual DbSet<Tag> Tags { get; set; }

	public virtual DbSet<User> Users { get; set; }

	public virtual DbSet<WebSetting> WebSettings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Bill>(entity =>
		{
			entity.HasKey(e => e.BillId).HasName("PK__Bills__6D903F034299EF9F");

			entity.HasIndex(e => e.CreatedAt, "IX_Bills_createdAt");

			entity.HasIndex(e => e.UserId, "IX_Bills_userId");

			entity.Property(e => e.BillId).HasColumnName("billId");
			entity.Property(e => e.Address)
				.HasMaxLength(500)
				.HasColumnName("address");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.DeliveryStatus)
				.HasMaxLength(50)
				.HasDefaultValue("pending")
				.HasColumnName("deliveryStatus");
			entity.Property(e => e.DiscountPercentage).HasColumnName("discountPercentage");
			entity.Property(e => e.PaymentMethod)
				.HasMaxLength(50)
				.HasDefaultValue("Cash on Delivery")
				.HasColumnName("paymentMethod");
			entity.Property(e => e.PaymentStatus)
				.HasMaxLength(50)
				.HasDefaultValue("unpaid")
				.HasColumnName("paymentStatus");
			entity.Property(e => e.Phone)
				.HasMaxLength(30)
				.HasColumnName("phone");
			entity.Property(e => e.ShippingMethod)
				.HasMaxLength(50)
				.HasDefaultValue("express")
				.HasColumnName("shippingMethod");
			entity.Property(e => e.Status)
				.HasDefaultValue(true)
				.HasColumnName("status");
			entity.Property(e => e.TotalAmount)
				.HasColumnType("decimal(18, 2)")
				.HasColumnName("totalAmount");
			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("updatedAt");
			entity.Property(e => e.UserId).HasColumnName("userId");

			entity.HasOne(d => d.User).WithMany(p => p.Bills)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Bills_Users");
		});

		modelBuilder.Entity<BillDetail>(entity =>
		{
			entity.HasKey(e => e.BillDetailId).HasName("PK__tmp_ms_x__C57B0C9A61F66BFC");

			entity.HasIndex(e => e.BillId, "IX_BillDetails_billId");

			entity.HasIndex(e => e.ProductId, "IX_BillDetails_productId");

			entity.Property(e => e.BillDetailId).HasColumnName("billDetailId");
			entity.Property(e => e.BillId).HasColumnName("billId");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.DiscountPercentage).HasColumnName("discountPercentage");
			entity.Property(e => e.ProductId).HasColumnName("productId");
			entity.Property(e => e.Quantity)
				.HasDefaultValue(1)
				.HasColumnName("quantity");
			entity.Property(e => e.SubTotal)
				.HasComputedColumnSql("([quantity]*[unitPrice])", true)
				.HasColumnType("decimal(29, 2)")
				.HasColumnName("subTotal");
			entity.Property(e => e.UnitPrice)
				.HasColumnType("decimal(18, 2)")
				.HasColumnName("unitPrice");
			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("updatedAt");

			entity.HasOne(d => d.Bill).WithMany(p => p.BillDetails)
				.HasForeignKey(d => d.BillId)
				.HasConstraintName("FK_BillDetails_Bills");

			entity.HasOne(d => d.Product).WithMany(p => p.BillDetails)
				.HasForeignKey(d => d.ProductId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_BillDetails_Products");
		});

		modelBuilder.Entity<Cart>(entity =>
		{
			entity.HasKey(e => e.CartId).HasName("PK__Carts__415B03B8DE4AE9E8");

			entity.HasIndex(e => e.UserId, "IX_Carts_userId");

			entity.Property(e => e.CartId).HasColumnName("cartId");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("updatedAt");
			entity.Property(e => e.UserId).HasColumnName("userId");

			entity.HasOne(d => d.User).WithMany(p => p.Carts)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Carts_Users");
		});

		modelBuilder.Entity<CartItem>(entity =>
		{
			entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__283983B6285A5BD1");

			entity.HasIndex(e => e.CartId, "IX_CartItems_cartId");

			entity.HasIndex(e => e.ProductId, "IX_CartItems_productId");

			entity.Property(e => e.CartItemId).HasColumnName("cartItemId");
			entity.Property(e => e.CartId).HasColumnName("cartId");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.ProductId).HasColumnName("productId");
			entity.Property(e => e.Quantity)
				.HasDefaultValue(1)
				.HasColumnName("quantity");
			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("updatedAt");

			entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
				.HasForeignKey(d => d.CartId)
				.HasConstraintName("FK_CartItems_Carts");

			entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
				.HasForeignKey(d => d.ProductId)
				.HasConstraintName("FK_CartItems_Products");
		});

		modelBuilder.Entity<Category>(entity =>
		{
			entity.HasKey(e => e.CategoryId).HasName("PK__Categori__23CAF1D824EB68C6");

			entity.HasIndex(e => e.CategoryName, "IX_Categories_categoryName");

			entity.Property(e => e.CategoryId).HasColumnName("categoryId");
			entity.Property(e => e.CategoryName)
				.HasMaxLength(150)
				.HasColumnName("categoryName");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.Description).HasColumnName("description");
			entity.Property(e => e.Status)
				.HasDefaultValue(true)
				.HasColumnName("status");
			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("updatedAt");
		});

		modelBuilder.Entity<Contact>(entity =>
		{
			entity.HasKey(e => e.ContactId).HasName("PK__Contacts__7121FD35D96D4451");

			entity.HasIndex(e => e.Email, "IX_Contacts_email");

			entity.Property(e => e.ContactId).HasColumnName("contactId");
			entity.Property(e => e.Content).HasColumnName("content");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.Email)
				.HasMaxLength(255)
				.HasColumnName("email");
			entity.Property(e => e.Name)
				.HasMaxLength(200)
				.HasColumnName("name");
			entity.Property(e => e.Phone)
				.HasMaxLength(50)
				.HasColumnName("phone");
			entity.Property(e => e.Status)
				.HasDefaultValue(true)
				.HasColumnName("status");
			entity.Property(e => e.Subject)
				.HasMaxLength(250)
				.HasColumnName("subject");
		});

		modelBuilder.Entity<Product>(entity =>
		{
			entity.HasKey(e => e.ProductId).HasName("PK__tmp_ms_x__2D10D16A4829F541");

			entity.HasIndex(e => e.CategoryId, "IX_Products_categoryId");

			entity.HasIndex(e => e.ProductName, "IX_Products_productName");

			entity.Property(e => e.ProductId).HasColumnName("productId");
			entity.Property(e => e.CategoryId).HasColumnName("categoryId");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.Description).HasColumnName("description");
			entity.Property(e => e.DiscountPercentage).HasColumnName("discountPercentage");
			entity.Property(e => e.Images)
				.IsUnicode(false)
				.HasColumnName("images");
			entity.Property(e => e.Price)
				.HasColumnType("decimal(18, 2)")
				.HasColumnName("price");
			entity.Property(e => e.ProductName)
				.HasMaxLength(250)
				.HasColumnName("productName");
			entity.Property(e => e.Status)
				.HasDefaultValue(true)
				.HasColumnName("status");
			entity.Property(e => e.Stock).HasColumnName("stock");
			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("updatedAt");
			entity.Property(e => e.Views)
				.HasDefaultValue(0)
				.HasColumnName("views");

			entity.HasOne(d => d.Category).WithMany(p => p.Products)
				.HasForeignKey(d => d.CategoryId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Products_Categories");

			entity.HasMany(d => d.Tags).WithMany(p => p.Products)
				.UsingEntity<Dictionary<string, object>>(
					"ProductTag",
					r => r.HasOne<Tag>().WithMany()
						.HasForeignKey("TagId")
						.HasConstraintName("FK_ProductTags_Tag"),
					l => l.HasOne<Product>().WithMany()
						.HasForeignKey("ProductId")
						.HasConstraintName("FK_ProductTags_Product"),
					j =>
					{
						j.HasKey("ProductId", "TagId").HasName("PK__ProductT__481F117F54DF7AB0");
						j.ToTable("ProductTags");
						j.IndexerProperty<int>("ProductId").HasColumnName("productId");
						j.IndexerProperty<int>("TagId").HasColumnName("tagId");
					});
		});

		modelBuilder.Entity<Review>(entity =>
		{
			entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__2ECD6E046D3EDD5C");

			entity.HasIndex(e => e.ProductId, "IX_Reviews_productId");

			entity.HasIndex(e => e.UserId, "IX_Reviews_userId");

			entity.Property(e => e.ReviewId).HasColumnName("reviewId");
			entity.Property(e => e.Content).HasColumnName("content");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.ProductId).HasColumnName("productId");
			entity.Property(e => e.Rating)
				.HasDefaultValue((byte)5)
				.HasColumnName("rating");
			entity.Property(e => e.Status)
				.HasDefaultValue(true)
				.HasColumnName("status");
			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("updatedAt");
			entity.Property(e => e.UserId).HasColumnName("userId");

			entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
				.HasForeignKey(d => d.ProductId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Reviews_Products");

			entity.HasOne(d => d.User).WithMany(p => p.Reviews)
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Reviews_Users");
		});

		modelBuilder.Entity<Tag>(entity =>
		{
			entity.HasKey(e => e.TagId).HasName("PK__Tags__50FC01571FB08230");

			entity.HasIndex(e => e.TagName, "IX_Tags_tagName");

			entity.Property(e => e.TagId).HasColumnName("tagId");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.Description).HasColumnName("description");
			entity.Property(e => e.Status)
				.HasDefaultValue(true)
				.HasColumnName("status");
			entity.Property(e => e.TagName)
				.HasMaxLength(150)
				.HasColumnName("tagName");
			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("updatedAt");
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CFF0118B670");

			entity.HasIndex(e => e.Email, "IX_Users_Email");

			entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164B06230CC").IsUnique();

			entity.Property(e => e.UserId).HasColumnName("userId");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.Email)
				.HasMaxLength(255)
				.HasColumnName("email");
			entity.Property(e => e.Password)
				.HasMaxLength(512)
				.HasColumnName("password");
			entity.Property(e => e.Role)
				.HasMaxLength(20)
				.HasDefaultValue("User")
				.HasColumnName("role");
			entity.Property(e => e.Status)
				.HasDefaultValue(true)
				.HasColumnName("status");
			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("updatedAt");
			entity.Property(e => e.Username)
				.HasMaxLength(100)
				.HasColumnName("username");
		});

		modelBuilder.Entity<WebSetting>(entity =>
		{
			entity.HasKey(e => e.SettingId).HasName("PK__WebSetti__097EE23C6447E7F7");

			entity.Property(e => e.SettingId).HasColumnName("settingId");
			entity.Property(e => e.Address)
				.HasMaxLength(500)
				.HasColumnName("address");
			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("createdAt");
			entity.Property(e => e.Email)
				.HasMaxLength(255)
				.HasColumnName("email");
			entity.Property(e => e.Facebook)
				.HasMaxLength(500)
				.HasColumnName("facebook");
			entity.Property(e => e.Favicon)
				.HasMaxLength(500)
				.HasColumnName("favicon");
			entity.Property(e => e.IsActive)
				.HasDefaultValue(true)
				.HasColumnName("isActive");
			entity.Property(e => e.LinkedIn)
				.HasMaxLength(500)
				.HasColumnName("linkedIn");
			entity.Property(e => e.Logo)
				.HasMaxLength(500)
				.HasColumnName("logo");
			entity.Property(e => e.Phone)
				.HasMaxLength(50)
				.HasColumnName("phone");
			entity.Property(e => e.SiteDescription)
				.HasMaxLength(500)
				.HasColumnName("siteDescription");
			entity.Property(e => e.SiteName)
				.HasMaxLength(200)
				.HasColumnName("siteName");
			entity.Property(e => e.Twitter)
				.HasMaxLength(500)
				.HasColumnName("twitter");
			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("(getdate())")
				.HasColumnName("updatedAt");
			entity.Property(e => e.Youtube)
				.HasMaxLength(500)
				.HasColumnName("youtube");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
