﻿// <auto-generated />
using System;
using Coalytics.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Coalytics.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180625225712_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Coalytics.Models.Auth.CoalyticsUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("CoalyticsUser");
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.CoalyticsProject", b =>
                {
                    b.Property<string>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectName");

                    b.HasKey("ProjectId");

                    b.ToTable("CoalyticsProjects");
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.CoalyticsTeam", b =>
                {
                    b.Property<string>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TeamName");

                    b.HasKey("TeamId");

                    b.ToTable("CoalyticsTeams");
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.FormComponent", b =>
                {
                    b.Property<string>("FormComponentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FormComponentName");

                    b.Property<string>("FormComponentTypeId");

                    b.HasKey("FormComponentId");

                    b.HasIndex("FormComponentTypeId");

                    b.ToTable("FormComponents");
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.FormComponentType", b =>
                {
                    b.Property<string>("FormComponentTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TypeName");

                    b.HasKey("FormComponentTypeId");

                    b.ToTable("FormComponentTypes");
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.ProjectFormComponent", b =>
                {
                    b.Property<string>("ProjectId");

                    b.Property<string>("FormComponentId");

                    b.HasKey("ProjectId", "FormComponentId");

                    b.HasIndex("FormComponentId");

                    b.ToTable("ProjectFormComponent");
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.ProjectTeam", b =>
                {
                    b.Property<string>("ProjectId");

                    b.Property<string>("TeamId");

                    b.HasKey("ProjectId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("ProjectTeam");
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.TeamUser", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("TeamId");

                    b.Property<string>("CoalyticsUserId");

                    b.HasKey("UserId", "TeamId");

                    b.HasIndex("CoalyticsUserId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.FormComponent", b =>
                {
                    b.HasOne("Coalytics.Models.Auth.Entity.FormComponentType")
                        .WithMany("FormComponents")
                        .HasForeignKey("FormComponentTypeId");
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.ProjectFormComponent", b =>
                {
                    b.HasOne("Coalytics.Models.Auth.Entity.FormComponent")
                        .WithMany("FormComponentProjects")
                        .HasForeignKey("FormComponentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Coalytics.Models.Auth.Entity.CoalyticsProject")
                        .WithMany("ProjectFormComponents")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.ProjectTeam", b =>
                {
                    b.HasOne("Coalytics.Models.Auth.Entity.CoalyticsProject")
                        .WithMany("ProjectTeams")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Coalytics.Models.Auth.Entity.CoalyticsTeam")
                        .WithMany("TeamProjects")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Coalytics.Models.Auth.Entity.TeamUser", b =>
                {
                    b.HasOne("Coalytics.Models.Auth.CoalyticsUser")
                        .WithMany("UserTeams")
                        .HasForeignKey("CoalyticsUserId");

                    b.HasOne("Coalytics.Models.Auth.Entity.CoalyticsTeam")
                        .WithMany("TeamUsers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Coalytics.Models.Auth.CoalyticsUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Coalytics.Models.Auth.CoalyticsUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Coalytics.Models.Auth.CoalyticsUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Coalytics.Models.Auth.CoalyticsUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
