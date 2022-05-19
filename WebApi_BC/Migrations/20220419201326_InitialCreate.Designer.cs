﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApi;

namespace WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220419201326_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("WebApi.Models.Chapter", b =>
                {
                    b.Property<int>("Id_chapter")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Id_course")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.HasKey("Id_chapter");

                    b.ToTable("WebApi.IDataContext.Chapters");
                });

            modelBuilder.Entity("WebApi.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .HasColumnType("text");

                    b.Property<bool>("Visibility")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("WebApi.IDataContext.Courses");
                });

            modelBuilder.Entity("WebApi.Models.Lesson", b =>
                {
                    b.Property<int>("Id_lesson")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Id_chapter")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id_lesson");

                    b.ToTable("WebApi.IDataContext.Lessons");
                });

            modelBuilder.Entity("WebApi.Models.User", b =>
                {
                    b.Property<string>("User_email")
                        .HasColumnType("text");

                    b.Property<string>("User_password")
                        .HasColumnType("text");

                    b.HasKey("User_email");

                    b.ToTable("WebApi.IDataContext.Users");
                });

            modelBuilder.Entity("WebApi.Models.UserPermission", b =>
                {
                    b.Property<string>("User_email")
                        .HasColumnType("text");

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("User_email");

                    b.ToTable("WebApi.IDataContext.UserPermissions");
                });

            modelBuilder.Entity("WebApi.Models.UserRole", b =>
                {
                    b.Property<string>("User_email")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("User_email");

                    b.ToTable("WebApi.IDataContext.UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}