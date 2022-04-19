using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace hotel_bellas_olas_api.Models
{
    public partial class BellasOlasHotelDbContext : DbContext
    {

        public BellasOlasHotelDbContext(DbContextOptions<BellasOlasHotelDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAdvertising> TbAdvertisings { get; set; } = null!;
        public virtual DbSet<TbCategoryFeature> TbCategoryFeatures { get; set; } = null!;
        public virtual DbSet<TbCategoryImage> TbCategoryImages { get; set; } = null!;
        public virtual DbSet<TbCreditCard> TbCreditCards { get; set; } = null!;
        public virtual DbSet<TbCreditCardUser> TbCreditCardUsers { get; set; } = null!;
        public virtual DbSet<TbHotel> TbHotels { get; set; } = null!;
        public virtual DbSet<TbImage> TbImages { get; set; } = null!;
        public virtual DbSet<TbOffer> TbOffers { get; set; } = null!;
        public virtual DbSet<TbReservation> TbReservations { get; set; } = null!;
        public virtual DbSet<TbRole> TbRoles { get; set; } = null!;
        public virtual DbSet<TbRoom> TbRooms { get; set; } = null!;
        public virtual DbSet<TbRoomCategory> TbRoomCategories { get; set; } = null!;
        public virtual DbSet<TbSeason> TbSeasons { get; set; } = null!;
        public virtual DbSet<TbUser> TbUsers { get; set; } = null!;

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=163.178.107.10;Initial Catalog=BELLAS_OLAS_HOTEL_DB;Persist Security Info=False;USER ID=laboratorios;Password=KmZpo.2796; MultipleActiveResultSets=False;TrustServerCertificate=False;");
            }
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbAdvertising>(entity =>
            {
                entity.HasKey(e => e.AdvertisingId)
                    .HasName("PK__tb_ADVER__94D0E0A3BD1C1CC9");

                entity.ToTable("tb_ADVERTISING", "ADMIN");

                entity.Property(e => e.AdvertisingId).HasColumnName("ADVERTISING_ID");

                entity.Property(e => e.AdLink)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("AD_LINK");

                entity.Property(e => e.ImageId).HasColumnName("IMAGE_ID");

                entity.Property(e => e.Info)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("INFO");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.TbAdvertisings)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_image_ad_id");
            });

            modelBuilder.Entity<TbCategoryFeature>(entity =>
            {
                entity.HasKey(e => e.FeatureId)
                    .HasName("PK__tb_CATEG__745D709D5699FF5A");

                entity.ToTable("tb_CATEGORY_FEATURE", "ADMIN");

                entity.Property(e => e.FeatureId).HasColumnName("FEATURE_ID");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Features)
                    .UsingEntity<Dictionary<string, object>>(
                        "TbFeatureCategory",
                        l => l.HasOne<TbRoomCategory>().WithMany().HasForeignKey("CategoryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_category_room"),
                        r => r.HasOne<TbCategoryFeature>().WithMany().HasForeignKey("FeatureId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_feature_feature"),
                        j =>
                        {
                            j.HasKey("FeatureId", "CategoryId").HasName("pk_feature_category");

                            j.ToTable("tb_FEATURE_CATEGORY", "ADMIN");

                            j.IndexerProperty<int>("FeatureId").HasColumnName("FEATURE_ID");

                            j.IndexerProperty<int>("CategoryId").HasColumnName("CATEGORY_ID");
                        });
            });

            modelBuilder.Entity<TbCategoryImage>(entity =>
            {
                entity.HasKey(e => e.ImageCategoryId)
                    .HasName("PK__tb_CATEG__579C857BEFCD62BA");

                entity.ToTable("tb_CATEGORY_IMAGE", "ADMIN");

                entity.Property(e => e.ImageCategoryId).HasColumnName("IMAGE_CATEGORY_ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TbCreditCard>(entity =>
            {
                entity.HasKey(e => e.CreditCardId)
                    .HasName("PK__tb_CREDI__99F1C4F64C75FC3A");

                entity.ToTable("tb_CREDIT_CARD", "ADMIN");

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

            modelBuilder.Entity<TbCreditCardUser>(entity =>
            {
                entity.HasKey(e => new { e.CreditCardId, e.UserId })
                    .HasName("pk_credit_card_user");

                entity.ToTable("tb_CREDIT_CARD_USER", "ADMIN");

                entity.Property(e => e.CreditCardId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CREDIT_CARD_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CreditCard)
                    .WithMany(p => p.TbCreditCardUsers)
                    .HasForeignKey(d => d.CreditCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_credit_card_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TbCreditCardUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_USER_ID");
            });

            modelBuilder.Entity<TbHotel>(entity =>
            {
                entity.HasKey(e => e.HotelId)
                    .HasName("PK__tb_HOTEL__21CB99F2B129B95A");

                entity.ToTable("tb_HOTEL", "ADMIN");

                entity.Property(e => e.HotelId).HasColumnName("HOTEL_ID");

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

            modelBuilder.Entity<TbImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__tb_IMAGE__7EA98689C4718168");

                entity.ToTable("tb_IMAGE", "ADMIN");

                entity.Property(e => e.ImageId).HasColumnName("IMAGE_ID");

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

                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Images)
                    .UsingEntity<Dictionary<string, object>>(
                        "TbImageCategory",
                        l => l.HasOne<TbCategoryImage>().WithMany().HasForeignKey("CategoryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_category_id_category"),
                        r => r.HasOne<TbImage>().WithMany().HasForeignKey("ImageId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_image_id_category"),
                        j =>
                        {
                            j.HasKey("ImageId", "CategoryId").HasName("pk_image_category");

                            j.ToTable("tb_IMAGE_CATEGORY", "ADMIN");

                            j.IndexerProperty<int>("ImageId").HasColumnName("IMAGE_ID");

                            j.IndexerProperty<int>("CategoryId").HasColumnName("CATEGORY_ID");
                        });
            });

            modelBuilder.Entity<TbOffer>(entity =>
            {
                entity.HasKey(e => e.OfferId)
                    .HasName("PK__tb_OFFER__96E0508B0A9ECF79");

                entity.ToTable("tb_OFFER", "ADMIN");

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

            modelBuilder.Entity<TbReservation>(entity =>
            {
                entity.HasKey(e => e.ReservationId)
                    .HasName("PK__tb_RESER__43C938EE7A935272");

                entity.ToTable("tb_RESERVATION", "ADMIN");

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
                    .WithMany(p => p.TbReservations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reservation_room_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TbReservations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reservation_user_id");
            });

            modelBuilder.Entity<TbRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__tb_ROLE__5AC4D222E8790EA6");

                entity.ToTable("tb_ROLE", "ADMIN");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IS_DELETED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ROLE_NAME");
            });

            modelBuilder.Entity<TbRoom>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("PK__tb_ROOM__2F3DD4821AD1E1A6");

                entity.ToTable("tb_ROOM", "ADMIN");

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
                    .WithMany(p => p.TbRooms)
                    .HasForeignKey(d => d.RoomCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_room_category");
            });

            modelBuilder.Entity<TbRoomCategory>(entity =>
            {
                entity.HasKey(e => e.RoomCategoryId)
                    .HasName("PK__tb_ROOM___CFE3C82737DDD8C3");

                entity.ToTable("tb_ROOM_CATEGORY", "ADMIN");

                entity.Property(e => e.RoomCategoryId).HasColumnName("ROOM_CATEGORY_ID");

                entity.Property(e => e.Cost).HasColumnName("COST");

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
                    .WithMany(p => p.TbRoomCategories)
                    .HasForeignKey(d => d.OfferId)
                    .HasConstraintName("fk_offer_room_category");
            });

            modelBuilder.Entity<TbSeason>(entity =>
            {
                entity.HasKey(e => e.SeasonId)
                    .HasName("PK__tb_SEASO__CC8E723C1A62FC53");

                entity.ToTable("tb_SEASON", "ADMIN");

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

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tb_USER__F3BEEBFFFFB16FC1");

                entity.ToTable("tb_USER", "ADMIN");

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
                    .WithMany(p => p.TbUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
