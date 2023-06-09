﻿// <auto-generated />
using BingeBot.Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BingeBot.Infrastructure.Databases.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230312201259_BingeBot")]
    partial class BingeBot
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BingeBot.Domain.Persons.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int>("TVmazeId")
                        .HasColumnType("int")
                        .HasColumnName("tvmaze_id");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("TVmazeId")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("BingeBot.Domain.ShowGenres.ShowGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int>("ShowId")
                        .HasColumnType("int")
                        .HasColumnName("show_id");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ShowId");

                    b.ToTable("ShowGenre");
                });

            modelBuilder.Entity("BingeBot.Domain.Shows.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("language");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("PremieredDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("premiered_date");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("summary");

                    b.Property<int?>("TVmazeId")
                        .HasColumnType("int")
                        .HasColumnName("tvmaze_id");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("TVmazeId")
                        .IsUnique()
                        .HasFilter("[tvmaze_id] IS NOT NULL");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("BingeBot.Domain.ShowGenres.ShowGenre", b =>
                {
                    b.HasOne("BingeBot.Domain.Shows.Show", "Show")
                        .WithMany("Genres")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("ForeignKey_Genre_Show");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("BingeBot.Domain.Shows.Show", b =>
                {
                    b.Navigation("Genres");
                });
#pragma warning restore 612, 618
        }
    }
}
