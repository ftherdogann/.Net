﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RelationShips;

#nullable disable

namespace RelationShips.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RelationShips.DataAccessLayer.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RelationShips.DataAccessLayer.ProductFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique()
                        .HasFilter("[ProductId] IS NOT NULL");

                    b.ToTable("ProductFeature");
                });

            modelBuilder.Entity("RelationShips.DataAccessLayer.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("RelationShips.DataAccessLayer.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("RelationShips.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category_Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Category_Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("StudentTeacherWithManyToMany", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("StudentTeacherWithManyToMany");
                });

            modelBuilder.Entity("RelationShips.DataAccessLayer.ProductFeature", b =>
                {
                    b.HasOne("RelationShips.Product", "Product")
                        .WithOne("ProductFeature")
                        .HasForeignKey("RelationShips.DataAccessLayer.ProductFeature", "ProductId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("RelationShips.Product", b =>
                {
                    b.HasOne("RelationShips.DataAccessLayer.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("StudentTeacherWithManyToMany", b =>
                {
                    b.HasOne("RelationShips.DataAccessLayer.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_StudentId");

                    b.HasOne("RelationShips.DataAccessLayer.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_TeacherId");
                });

            modelBuilder.Entity("RelationShips.DataAccessLayer.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("RelationShips.Product", b =>
                {
                    b.Navigation("ProductFeature")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}