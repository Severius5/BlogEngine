﻿// <auto-generated />
using System;
using BlogEngine.Storage.ProviderContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BlogEngine.Migrations.PostgreMigrations
{
    [DbContext(typeof(PostgreBlogContext))]
    [Migration("20190324162812_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BlogEngine.Storage.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("NormalizedTitle")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<DateTime?>("PublicationDate");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("NormalizedTitle")
                        .IsUnique();

                    b.ToTable("posts");
                });

            modelBuilder.Entity("BlogEngine.Storage.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bio")
                        .HasMaxLength(1024);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("DetailsStamp");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedUsername")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .IsUnique();

                    b.HasIndex("NormalizedUsername")
                        .IsUnique();

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "Write here some information about yourself.  You can use markdown here.",
                            CreationDate = new DateTime(2018, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified),
                            DetailsStamp = "DA56E937-FF2C-4401-9DC2-E353DD474346",
                            Email = "admin@admin.admin",
                            IsAdmin = true,
                            NormalizedEmail = "ADMIN@ADMIN.ADMIN",
                            NormalizedUsername = "ADMINISTRATOR",
                            Password = "AQAAAAEAACcaAAAAECekU4gLOtHZ4lSHACsEr1UjxJ5fo2dUjXTM1Rq8ZeRZzkcq+h8zWlLgtJ2LGpUU1w==",
                            Slug = "administrator",
                            Username = "Administrator"
                        });
                });

            modelBuilder.Entity("BlogEngine.Storage.Entities.Post", b =>
                {
                    b.HasOne("BlogEngine.Storage.Entities.User", "Author")
                        .WithMany("CreatedPosts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}