﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserMgr.Infrastracture.DbContexts;

#nullable disable

namespace UserMgr.Infrastracture.Migrations
{
    [DbContext(typeof(UserDbContext))]
    partial class UserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserMgr.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("passwordHash")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("T_Users", (string)null);
                });

            modelBuilder.Entity("UserMgr.Domain.Entities.UserAccessFail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LockoutEnd")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("lockOut")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("T_UserAccessFails", (string)null);
                });

            modelBuilder.Entity("UserMgr.Domain.Entities.UserLoginHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("T_UserLoginHistories", (string)null);
                });

            modelBuilder.Entity("UserMgr.Domain.Entities.User", b =>
                {
                    b.OwnsOne("UserMgr.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(20)
                                .IsUnicode(false)
                                .HasColumnType("varchar(20)");

                            b1.Property<int>("RegionCode")
                                .HasMaxLength(5)
                                .IsUnicode(false)
                                .HasColumnType("int");

                            b1.HasKey("UserId");

                            b1.ToTable("T_Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("UserMgr.Domain.Entities.UserAccessFail", b =>
                {
                    b.HasOne("UserMgr.Domain.Entities.User", "User")
                        .WithOne("AccessFail")
                        .HasForeignKey("UserMgr.Domain.Entities.UserAccessFail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserMgr.Domain.Entities.UserLoginHistory", b =>
                {
                    b.OwnsOne("UserMgr.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<long>("UserLoginHistoryId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(20)
                                .IsUnicode(false)
                                .HasColumnType("varchar(20)");

                            b1.Property<int>("RegionCode")
                                .HasMaxLength(5)
                                .IsUnicode(false)
                                .HasColumnType("int");

                            b1.HasKey("UserLoginHistoryId");

                            b1.ToTable("T_UserLoginHistories");

                            b1.WithOwner()
                                .HasForeignKey("UserLoginHistoryId");
                        });

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("UserMgr.Domain.Entities.User", b =>
                {
                    b.Navigation("AccessFail")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
