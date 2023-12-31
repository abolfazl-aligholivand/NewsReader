﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewsReader.Domain.Data;

#nullable disable

namespace NewsReader.Migrations
{
    [DbContext(typeof(NewsReaderContext))]
    [Migration("20231017094222_ModifyNewsGuid")]
    partial class ModifyNewsGuid
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Arabic_CI_AS")
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NewsReader.Domain.Models.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("NewsReader.Domain.Models.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Date")
                        .IsUnicode(false)
                        .HasColumnType("datetime2")
                        .IsFixedLength();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FKCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("FKWebsiteId")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Media")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NewsGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FKCategoryId");

                    b.HasIndex("FKWebsiteId");

                    b.ToTable("Newses");
                });

            modelBuilder.Entity("NewsReader.Domain.Models.NewsCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("NewsCategories");
                });

            modelBuilder.Entity("NewsReader.Domain.Models.NewsKeyword", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("NewsKeywords");
                });

            modelBuilder.Entity("NewsReader.Domain.Models.Website", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FKCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("FeedLink")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("FKCategoryId");

                    b.ToTable("Websites");
                });

            modelBuilder.Entity("NewsReader.Domain.Models.WebsiteCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("WebsiteCategories");
                });

            modelBuilder.Entity("NewsReader.Domain.Models.News", b =>
                {
                    b.HasOne("NewsReader.Domain.Models.NewsCategory", "FKCategory")
                        .WithMany("Newses")
                        .HasForeignKey("FKCategoryId")
                        .IsRequired();

                    b.HasOne("NewsReader.Domain.Models.Website", "FKWebsite")
                        .WithMany("Newses")
                        .HasForeignKey("FKWebsiteId")
                        .IsRequired();

                    b.Navigation("FKCategory");

                    b.Navigation("FKWebsite");
                });

            modelBuilder.Entity("NewsReader.Domain.Models.Website", b =>
                {
                    b.HasOne("NewsReader.Domain.Models.WebsiteCategory", "FKCategory")
                        .WithMany("Websites")
                        .HasForeignKey("FKCategoryId")
                        .IsRequired();

                    b.Navigation("FKCategory");
                });

            modelBuilder.Entity("NewsReader.Domain.Models.NewsCategory", b =>
                {
                    b.Navigation("Newses");
                });

            modelBuilder.Entity("NewsReader.Domain.Models.Website", b =>
                {
                    b.Navigation("Newses");
                });

            modelBuilder.Entity("NewsReader.Domain.Models.WebsiteCategory", b =>
                {
                    b.Navigation("Websites");
                });
#pragma warning restore 612, 618
        }
    }
}
