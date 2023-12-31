﻿// <auto-generated />
using BankKata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankKata.Migrations
{
    [DbContext(typeof(BankKataDbContext))]
    [Migration("20240107161313_initialMigrations")]
    partial class initialMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BankKata.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Balance = 0
                        },
                        new
                        {
                            Id = 2,
                            Balance = 500
                        });
                });

            modelBuilder.Entity("BankKata.Movement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Movements");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            Amount = 0,
                            Balance = 0
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 2,
                            Amount = 500,
                            Balance = 500
                        });
                });

            modelBuilder.Entity("BankKata.Movement", b =>
                {
                    b.HasOne("BankKata.Account", null)
                        .WithMany("Movements")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BankKata.Account", b =>
                {
                    b.Navigation("Movements");
                });
#pragma warning restore 612, 618
        }
    }
}
