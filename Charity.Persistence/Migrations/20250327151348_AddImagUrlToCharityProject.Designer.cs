﻿// <auto-generated />
using System;
using Charity.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Charity.Persistence.Migrations
{
    [DbContext(typeof(CharityDbContext))]
    [Migration("20250327151348_AddImagUrlToCharityProject")]
    partial class AddImagUrlToCharityProject
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Charity.Domain.Entities.AidDistribution", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BeneficiaryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InKindDonationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MonetaryDonationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VolunteerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BeneficiaryId");

                    b.HasIndex("InKindDonationId");

                    b.HasIndex("MonetaryDonationId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("AidDistributions", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.AssistanceRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BeneficiaryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InKindDonationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BeneficiaryId");

                    b.HasIndex("InKindDonationId");

                    b.ToTable("AssistanceRequests", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.CharityProject", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManagerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ProjectStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("TargetAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CharityProjects", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.IdentityEntities.CharityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("CharityRoles", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.IdentityEntities.CharityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("CharityUsers", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.IdentityEntities.JwtToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRefreshJWTUsed")
                        .HasColumnType("bit");

                    b.Property<string>("JWT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("JWTExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshJWT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshJWTExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RefreshJWTRevokedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("JwtTokens", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.InKindDonation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DonorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageUrls")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAllocated")
                        .HasColumnType("bit");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DonorId");

                    b.HasIndex("ProjectId");

                    b.ToTable("InKindDonations", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.MonetaryDonation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 4)
                        .HasColumnType("decimal(10,4)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DonorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsAllocated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DonorId");

                    b.HasIndex("ProjectId");

                    b.ToTable("MonetaryDonations", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.Notification", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Notifications", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.ProjectVolunteer", b =>
                {
                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VolunteerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ProjectId", "VolunteerId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("ProjectVolunteers", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.UserNotification", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NotificationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "NotificationId");

                    b.HasIndex("NotificationId");

                    b.ToTable("UserNotifications", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.UserVolunteerActivity", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VolunteerActivityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "VolunteerActivityId");

                    b.HasIndex("VolunteerActivityId");

                    b.ToTable("UserVolunteerActivities", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.VolunteerActivity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ActivityDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrganizerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("VolunteerActivities", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.VolunteerApplication", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("VolunteerActivityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VolunteerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("VolunteerActivityId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("VolunteerApplications", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Charity.Domain.Entities.AidDistribution", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "Beneficiary")
                        .WithMany("ReceivedAids")
                        .HasForeignKey("BeneficiaryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Charity.Domain.Entities.InKindDonation", "InKindDonation")
                        .WithMany("AidDistributions")
                        .HasForeignKey("InKindDonationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Charity.Domain.Entities.MonetaryDonation", "MonetaryDonation")
                        .WithMany("AidDistributions")
                        .HasForeignKey("MonetaryDonationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "Volunteer")
                        .WithMany("DistributedAids")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Beneficiary");

                    b.Navigation("InKindDonation");

                    b.Navigation("MonetaryDonation");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Charity.Domain.Entities.AssistanceRequest", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "Beneficiary")
                        .WithMany("AssistanceRequests")
                        .HasForeignKey("BeneficiaryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Charity.Domain.Entities.InKindDonation", "InKindDonation")
                        .WithMany("AssistanceRequests")
                        .HasForeignKey("InKindDonationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Beneficiary");

                    b.Navigation("InKindDonation");
                });

            modelBuilder.Entity("Charity.Domain.Entities.CharityProject", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "Manager")
                        .WithMany("ManagedProjects")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Charity.Domain.Entities.IdentityEntities.JwtToken", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "User")
                        .WithMany("JwtTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Charity.Domain.Entities.InKindDonation", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "Donor")
                        .WithMany("InKindDonations")
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Charity.Domain.Entities.CharityProject", "Project")
                        .WithMany("InKindDonations")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Donor");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Charity.Domain.Entities.MonetaryDonation", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "Donor")
                        .WithMany("MonetaryDonations")
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Charity.Domain.Entities.CharityProject", "Project")
                        .WithMany("MonetaryDonations")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Donor");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Charity.Domain.Entities.Notification", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "Sender")
                        .WithMany("SentNotifications")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Charity.Domain.Entities.ProjectVolunteer", b =>
                {
                    b.HasOne("Charity.Domain.Entities.CharityProject", "Project")
                        .WithMany("ProjectVolunteers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "Volunteer")
                        .WithMany("ProjectVolunteers")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("Charity.Domain.Entities.UserNotification", b =>
                {
                    b.HasOne("Charity.Domain.Entities.Notification", "Notification")
                        .WithMany("UserNotifications")
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "User")
                        .WithMany("ReceivedNotifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Notification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Charity.Domain.Entities.UserVolunteerActivity", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "User")
                        .WithMany("VolunteerActivities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Charity.Domain.Entities.VolunteerActivity", "VolunteerActivity")
                        .WithMany("Volunteers")
                        .HasForeignKey("VolunteerActivityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("VolunteerActivity");
                });

            modelBuilder.Entity("Charity.Domain.Entities.VolunteerActivity", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "Organizer")
                        .WithMany("OrganizedVolunteerActivities")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("Charity.Domain.Entities.VolunteerApplication", b =>
                {
                    b.HasOne("Charity.Domain.Entities.VolunteerActivity", "VolunteerActivity")
                        .WithMany("VolunteerApplications")
                        .HasForeignKey("VolunteerActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", "Volunteer")
                        .WithMany("VolunteerApplications")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Volunteer");

                    b.Navigation("VolunteerActivity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Charity.Domain.Entities.IdentityEntities.CharityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Charity.Domain.Entities.CharityProject", b =>
                {
                    b.Navigation("InKindDonations");

                    b.Navigation("MonetaryDonations");

                    b.Navigation("ProjectVolunteers");
                });

            modelBuilder.Entity("Charity.Domain.Entities.IdentityEntities.CharityUser", b =>
                {
                    b.Navigation("AssistanceRequests");

                    b.Navigation("DistributedAids");

                    b.Navigation("InKindDonations");

                    b.Navigation("JwtTokens");

                    b.Navigation("ManagedProjects");

                    b.Navigation("MonetaryDonations");

                    b.Navigation("OrganizedVolunteerActivities");

                    b.Navigation("ProjectVolunteers");

                    b.Navigation("ReceivedAids");

                    b.Navigation("ReceivedNotifications");

                    b.Navigation("SentNotifications");

                    b.Navigation("VolunteerActivities");

                    b.Navigation("VolunteerApplications");
                });

            modelBuilder.Entity("Charity.Domain.Entities.InKindDonation", b =>
                {
                    b.Navigation("AidDistributions");

                    b.Navigation("AssistanceRequests");
                });

            modelBuilder.Entity("Charity.Domain.Entities.MonetaryDonation", b =>
                {
                    b.Navigation("AidDistributions");
                });

            modelBuilder.Entity("Charity.Domain.Entities.Notification", b =>
                {
                    b.Navigation("UserNotifications");
                });

            modelBuilder.Entity("Charity.Domain.Entities.VolunteerActivity", b =>
                {
                    b.Navigation("VolunteerApplications");

                    b.Navigation("Volunteers");
                });
#pragma warning restore 612, 618
        }
    }
}
