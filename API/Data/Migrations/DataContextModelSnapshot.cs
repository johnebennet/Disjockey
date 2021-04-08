﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("API.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DiscordId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Models.AppUserTrack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackPlays");
                });

            modelBuilder.Entity("API.Models.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("API.Models.PlaylistTrack", b =>
                {
                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("TrackId", "PlaylistId");

                    b.HasIndex("CreatedById");

                    b.ToTable("PlaylistTrack");
                });

            modelBuilder.Entity("API.Models.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ChannelTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LargeThumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MediumThumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SmallThumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YoutubeId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("API.Models.TrackLike", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<bool>("Liked")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackLikes");
                });

            modelBuilder.Entity("API.Models.AppUserTrack", b =>
                {
                    b.HasOne("API.Models.AppUser", "User")
                        .WithMany("Tracks")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Track", "Track")
                        .WithMany("AppUsers")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Models.PlaylistTrack", b =>
                {
                    b.HasOne("API.Models.AppUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("API.Models.Playlist", "Playlist")
                        .WithMany("Tracks")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Track", "Track")
                        .WithMany("Playlists")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Playlist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("API.Models.TrackLike", b =>
                {
                    b.HasOne("API.Models.Track", "Track")
                        .WithMany("Likes")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.AppUser", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Models.AppUser", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("API.Models.Playlist", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("API.Models.Track", b =>
                {
                    b.Navigation("AppUsers");

                    b.Navigation("Likes");

                    b.Navigation("Playlists");
                });
#pragma warning restore 612, 618
        }
    }
}
