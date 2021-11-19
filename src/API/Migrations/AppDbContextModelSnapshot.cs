﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Data.Entities.Category", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ParrentCode")
                        .HasColumnType("text");

                    b.HasKey("Code");

                    b.HasIndex("ParrentCode");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Data.Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("BeneficiaryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CategoryCode")
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Date")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)");

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Mcc")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryCode");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("Data.Entities.TransactionSplit", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("CategoryCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TransactionId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryCode");

                    b.HasIndex("TransactionId");

                    b.ToTable("splits");
                });

            modelBuilder.Entity("Data.Entities.Category", b =>
                {
                    b.HasOne("Data.Entities.Category", "ParrentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParrentCode")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ParrentCategory");
                });

            modelBuilder.Entity("Data.Entities.Transaction", b =>
                {
                    b.HasOne("Data.Entities.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryCode")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Data.Entities.TransactionSplit", b =>
                {
                    b.HasOne("Data.Entities.Category", "Category")
                        .WithMany("TransactionSplits")
                        .HasForeignKey("CategoryCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Transaction", "Transaction")
                        .WithMany("Splits")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Category");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Data.Entities.Category", b =>
                {
                    b.Navigation("SubCategories");

                    b.Navigation("Transactions");

                    b.Navigation("TransactionSplits");
                });

            modelBuilder.Entity("Data.Entities.Transaction", b =>
                {
                    b.Navigation("Splits");
                });
#pragma warning restore 612, 618
        }
    }
}
