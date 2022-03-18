﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace ProjekatWEB.Migrations
{
    [DbContext(typeof(ProdajaDiskovaContext))]
    partial class ProdajaDiskovaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Film", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Ocena")
                        .HasColumnType("real");

                    b.Property<int?>("ReziserID")
                        .HasColumnType("int");

                    b.Property<int?>("ZanrID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ReziserID");

                    b.HasIndex("ZanrID");

                    b.ToTable("Film");
                });

            modelBuilder.Entity("Models.Glumac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Glumac");
                });

            modelBuilder.Entity("Models.Reziser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Reziser");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FilmoviID")
                        .HasColumnType("int");

                    b.Property<int?>("GlumciID")
                        .HasColumnType("int");

                    b.Property<string>("Uloga")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FilmoviID");

                    b.HasIndex("GlumciID");

                    b.ToTable("Spoj");
                });

            modelBuilder.Entity("Models.Zanr", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Zanr");
                });

            modelBuilder.Entity("Models.Film", b =>
                {
                    b.HasOne("Models.Reziser", "Reziser")
                        .WithMany("Filmovi")
                        .HasForeignKey("ReziserID");

                    b.HasOne("Models.Zanr", "Zanr")
                        .WithMany("Filmovi")
                        .HasForeignKey("ZanrID");

                    b.Navigation("Reziser");

                    b.Navigation("Zanr");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.HasOne("Models.Film", "Filmovi")
                        .WithMany("FilmGlumac")
                        .HasForeignKey("FilmoviID");

                    b.HasOne("Models.Glumac", "Glumci")
                        .WithMany("Filmovi")
                        .HasForeignKey("GlumciID");

                    b.Navigation("Filmovi");

                    b.Navigation("Glumci");
                });

            modelBuilder.Entity("Models.Film", b =>
                {
                    b.Navigation("FilmGlumac");
                });

            modelBuilder.Entity("Models.Glumac", b =>
                {
                    b.Navigation("Filmovi");
                });

            modelBuilder.Entity("Models.Reziser", b =>
                {
                    b.Navigation("Filmovi");
                });

            modelBuilder.Entity("Models.Zanr", b =>
                {
                    b.Navigation("Filmovi");
                });
#pragma warning restore 612, 618
        }
    }
}
