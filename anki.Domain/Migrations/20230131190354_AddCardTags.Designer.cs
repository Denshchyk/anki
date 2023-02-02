﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using anki.Domain;

#nullable disable

namespace anki.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230131190354_AddCardTags")]
    partial class AddCardTags
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("anki.Domain.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Back")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Front")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("anki.Domain.CardTag", b =>
                {
                    b.Property<Guid>("CardId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.HasKey("CardId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("CardTags");
                });

            modelBuilder.Entity("anki.Domain.Tag", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("anki.Domain.CardTag", b =>
                {
                    b.HasOne("anki.Domain.Card", "Card")
                        .WithMany("CardTags")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("anki.Domain.Tag", "Tag")
                        .WithMany("CardTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("anki.Domain.Card", b =>
                {
                    b.Navigation("CardTags");
                });

            modelBuilder.Entity("anki.Domain.Tag", b =>
                {
                    b.Navigation("CardTags");
                });
#pragma warning restore 612, 618
        }
    }
}