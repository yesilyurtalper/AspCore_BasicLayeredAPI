﻿// <auto-generated />
using System;
using BasicLayeredService.API.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BasicLayeredService.API.Migrations
{
    [DbContext(typeof(PostingAPIDbContext))]
    [Migration("20230808211604_m1")]
    partial class m1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BasicLayeredService.API.Domain.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("varchar(10000)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Author");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "alpery",
                            DateCreated = new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(832),
                            DateModified = new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(840),
                            Description = "Some user post by alpery",
                            Title = "postA"
                        },
                        new
                        {
                            Id = 2,
                            Author = "alpery",
                            DateCreated = new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(841),
                            DateModified = new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(841),
                            Description = "Some user post by alpery",
                            Title = "postB"
                        },
                        new
                        {
                            Id = 3,
                            Author = "alpery",
                            DateCreated = new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(842),
                            DateModified = new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(843),
                            Description = "Some user post by alpery",
                            Title = "postC"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
