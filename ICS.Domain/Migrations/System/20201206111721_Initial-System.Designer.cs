﻿// <auto-generated />
using System;
using ICS.Domain.Data.Adapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ICS.Domain.Migrations.System
{
    [DbContext(typeof(SystemContext))]
    [Migration("20201206111721_Initial-System")]
    partial class InitialSystem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ICS.Domain.Entities.System.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Uid");

                    b.Property<long>("OrdinalNumber")
                        .HasColumnType("bigint")
                        .HasColumnName("OrdinalNumber");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea")
                        .HasColumnName("Avatar");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("UserUid");

                    b.Property<string>("WebPages")
                        .HasColumnType("text")
                        .HasColumnName("WebPages");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles", "system");
                });

            modelBuilder.Entity("ICS.Domain.Entities.System.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("RoleName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ICS.Domain.Entities.System.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Uid");

                    b.Property<string>("AccountName")
                        .HasColumnType("text")
                        .HasColumnName("Account");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("Password");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uuid")
                        .HasColumnName("ProfileUid");

                    b.HasKey("Id");

                    b.ToTable("Users", "system");
                });

            modelBuilder.Entity("ICS.Domain.Entities.System.Profile", b =>
                {
                    b.HasOne("ICS.Domain.Entities.System.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("ICS.Domain.Entities.System.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ICS.Domain.Entities.System.User", b =>
                {
                    b.Navigation("Profile");
                });
#pragma warning restore 612, 618
        }
    }
}