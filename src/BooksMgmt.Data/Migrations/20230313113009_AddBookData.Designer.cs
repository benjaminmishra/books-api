﻿// <auto-generated />
using System;
using BooksMgmt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BooksMgmt.Data.Migrations
{
    [DbContext(typeof(BooksDbContext))]
    [Migration("20230313113009_AddBookData")]
    partial class AddBookData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.1.23111.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BooksMgmt.Data.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 50,
                            DateOfBirth = new DateTime(1892, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = "Male",
                            Name = "J.R.R. Tolkien"
                        },
                        new
                        {
                            Id = 2,
                            Age = 50,
                            DateOfBirth = new DateTime(1892, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = "Male",
                            Name = "J.R.R. Martin"
                        },
                        new
                        {
                            Id = 5,
                            Age = 50,
                            DateOfBirth = new DateTime(1989, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = "Female",
                            Name = "JK Rowling"
                        },
                        new
                        {
                            Id = 500,
                            Age = 25,
                            DateOfBirth = new DateTime(2000, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = "Female",
                            Name = "XYZ Author"
                        });
                });

            modelBuilder.Entity("BooksMgmt.Data.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 5,
                            Description = "A book about the adventures of Harry Potter",
                            Genre = "Adventure",
                            Isbn = "12AVC456",
                            Title = "Harry Potter"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            Description = "A book about the adventures of Harry Potter",
                            Genre = "Fantasy",
                            Isbn = "89AAVC456",
                            Title = "The Lord Of The Rings"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Description = "A book about Westrose and its rulers",
                            Genre = "Fantasy",
                            Isbn = "ABC45690",
                            Title = "Song of Ice and Fire"
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 500,
                            Description = "A book about Westrose and its rulers",
                            Genre = "Fantasy",
                            Isbn = "ABC45690",
                            Title = "Song of Ice and Fire2"
                        });
                });

            modelBuilder.Entity("BooksMgmt.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "xyz@email.com",
                            Name = "XYZ",
                            Password = "password1",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "efg@email.com",
                            Name = "EFG",
                            Password = "password2",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("BooksMgmt.Data.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("BooksMgmt.Data.Models.Book", b =>
                {
                    b.HasOne("BooksMgmt.Data.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("BooksMgmt.Data.Models.User", b =>
                {
                    b.HasOne("BooksMgmt.Data.Models.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
