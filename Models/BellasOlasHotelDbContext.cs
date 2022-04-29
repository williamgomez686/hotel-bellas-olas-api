using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace hotel_bellas_olas_api.Models
{
    public partial class BellasOlasHotelDbContext : DbContext
    {
        public BellasOlasHotelDbContext()
        {
        }

        public BellasOlasHotelDbContext(DbContextOptions<BellasOlasHotelDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Advertising> Advertisings { get; set; } = null!;
        public virtual DbSet<Creditcard> Creditcards { get; set; } = null!;
        public virtual DbSet<Creditcarduser> Creditcardusers { get; set; } = null!;
        public virtual DbSet<Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<Hotelfeature> Hotelfeatures { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Imagecatalog> Imagecatalogs { get; set; } = null!;
        public virtual DbSet<Offer> Offers { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Roomcategory> Roomcategories { get; set; } = null!;
        public virtual DbSet<Season> Seasons { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=163.178.107.10;Initial Catalog=BELLAS_OLAS_HOTEL_DB;Persist Security Info=False;USER ID=laboratorios;Password=KmZpo.2796; MultipleActiveResultSets=False;TrustServerCertificate=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertising>(entity =>
            {
                entity.ToTable("ADVERTISING", "ADMIN");

                entity.Property(e => e.AdvertisingId).HasColumnName("ADVERTISING_ID");

                entity.Property(e => e.AdLink)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("AD_LINK");

                entity.Property(e => e.ImageId).HasColumnName("IMAGE_ID");

                entity.Property(e => e.Info)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("INFO");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Advertisings)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_image_ad_id");
            });

            modelBuilder.Entity<Creditcard>(entity =>
            {
                entity.ToTable("CREDITCARD", "ADMIN");

                entity.Property(e => e.CreditCardId).HasColumnName("CREDIT_CARD_ID");

                entity.Property(e => e.CardDate)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("CARD_DATE");

                entity.Property(e => e.CardNumber).HasColumnName("CARD_NUMBER");

                entity.Property(e => e.CardPin).HasColumnName("CARD_PIN");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Creditcarduser>(entity =>
            {
                entity.HasKey(e => new { e.CreditCardId, e.UserId })
                    .HasName("pk_credit_card_user");

                entity.ToTable("CREDITCARDUSER", "ADMIN");

                entity.Property(e => e.CreditCardId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CREDIT_CARD_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CreditCard)
                    .WithMany(p => p.Creditcardusers)
                    .HasForeignKey(d => d.CreditCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_credit_card_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Creditcardusers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_USER_ID");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("HOTEL", "ADMIN");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

                entity.Property(e => e.AboutUs)
                    .IsUnicode(false)
                    .HasColumnName("ABOUT_US");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("PHONE_NUMBER");
            });

            modelBuilder.Entity<Hotelfeature>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("HOTELFEATURE", "ADMIN");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.FeatureId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FEATURE_ID");

                entity.Property(e => e.ImageId).HasColumnName("IMAGE_ID");

                entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("IMAGE", "ADMIN");

                entity.Property(e => e.ImageId).HasColumnName("IMAGE_ID");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Content)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONTENT");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_image_catalog_id");
            });

            modelBuilder.Entity<Imagecatalog>(entity =>
            {
                entity.HasKey(e => e.ImageCategoryId)
                    .HasName("PK__IMAGECAT__579C857B36AECF72");

                entity.ToTable("IMAGECATALOG", "ADMIN");

                entity.Property(e => e.ImageCategoryId).HasColumnName("IMAGE_CATEGORY_ID");

                entity.Property(e => e.CatalogName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("CATALOG_NAME");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("OFFER", "ADMIN");

                entity.Property(e => e.OfferId).HasColumnName("OFFER_ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasColumnName("NAME");

                entity.Property(e => e.OfferPercent).HasColumnName("OFFER_PERCENT");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("START_DATE");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("RESERVATION", "ADMIN");

                entity.Property(e => e.ReservationId).HasColumnName("RESERVATION_ID");

                entity.Property(e => e.DepartureDate)
                    .HasColumnType("date")
                    .HasColumnName("DEPARTURE_DATE");

                entity.Property(e => e.EntryDate)
                    .HasColumnType("date")
                    .HasColumnName("ENTRY_DATE");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReservationNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("RESERVATION_NUMBER");

                entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reservation_room_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reservation_user_id");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE", "ADMIN");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_NAME");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("ROOM", "ADMIN");

                entity.Property(e => e.RoomId).HasColumnName("ROOM_ID");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RoomCategoryId).HasColumnName("ROOM_CATEGORY_ID");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ROOM_NAME");

                entity.Property(e => e.RoomNumber).HasColumnName("ROOM_NUMBER");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.RoomCategory)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_room_category");
            });

            modelBuilder.Entity<Roomcategory>(entity =>
            {
                entity.ToTable("ROOMCATEGORY", "ADMIN");

                entity.Property(e => e.RoomCategoryId).HasColumnName("ROOM_CATEGORY_ID");

                entity.Property(e => e.Cost)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COST");

                entity.Property(e => e.Description)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.ImageId).HasColumnName("IMAGE_ID");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.OfferId).HasColumnName("OFFER_ID");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.Roomcategories)
                    .HasForeignKey(d => d.OfferId)
                    .HasConstraintName("fk_offer_room_category");
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.ToTable("SEASON", "ADMIN");

                entity.Property(e => e.SeasonId).HasColumnName("SEASON_ID");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PercentApply).HasColumnName("PERCENT_APPLY");

                entity.Property(e => e.Type)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("_USER", "ADMIN");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
