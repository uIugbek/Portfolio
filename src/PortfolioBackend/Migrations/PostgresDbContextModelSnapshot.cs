using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PortfolioBackend.Core.DAL;

namespace PortfolioBackend.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    partial class PostgresDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("PortfolioBackend.Core.DAL.Culture", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Cultures");
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.LocalizedString", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CultureId");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("LocalizedStrings");
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("Code");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int?>("OwnerId");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.Permission_Locale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CultureId");

                    b.Property<string>("Description")
                        .HasMaxLength(1023);

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LocalizableEntityId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int?>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("CultureId");

                    b.HasIndex("LocalizableEntityId");

                    b.ToTable("Permission_Locales");
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int?>("OwnerId");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.Role_Locale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CultureId");

                    b.Property<string>("Description")
                        .HasMaxLength(1024);

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LocalizableEntityId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int?>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("CultureId");

                    b.HasIndex("LocalizableEntityId");

                    b.ToTable("Role_Locales");
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.RoleInPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("IsAccessible");

                    b.Property<int>("PermissionId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleInPermissions");
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<bool>("IsBlocked");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Login")
                        .HasMaxLength(50);

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("OwnerId");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(50);

                    b.Property<string>("PasswordSalt")
                        .HasMaxLength(50);

                    b.Property<string>("Photo")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Login", "Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.UserInRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserInRoles");
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.Permission_Locale", b =>
                {
                    b.HasOne("PortfolioBackend.Core.DAL.Culture", "Culture")
                        .WithMany()
                        .HasForeignKey("CultureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PortfolioBackend.Core.DAL.Permission", "LocalizableEntity")
                        .WithMany("Localizations")
                        .HasForeignKey("LocalizableEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.Role_Locale", b =>
                {
                    b.HasOne("PortfolioBackend.Core.DAL.Culture", "Culture")
                        .WithMany()
                        .HasForeignKey("CultureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PortfolioBackend.Core.DAL.Role", "LocalizableEntity")
                        .WithMany("Localizations")
                        .HasForeignKey("LocalizableEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.RoleInPermission", b =>
                {
                    b.HasOne("PortfolioBackend.Core.DAL.Permission", "Permission")
                        .WithMany("RoleInPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PortfolioBackend.Core.DAL.Role", "Role")
                        .WithMany("RoleInPermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PortfolioBackend.Core.DAL.UserInRole", b =>
                {
                    b.HasOne("PortfolioBackend.Core.DAL.Role", "Role")
                        .WithMany("UserInRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PortfolioBackend.Core.DAL.User", "User")
                        .WithMany("UserInRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
