﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<long>("DiscordId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Entities.AppUserTrack", b =>
                {
                    b.Property<int>("AppUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrackId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastPlayed")
                        .HasColumnType("TEXT");

                    b.Property<int>("TimesPlayed")
                        .HasColumnType("INTEGER");

                    b.HasKey("AppUserId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("UserTracks");
                });

            modelBuilder.Entity("API.Entities.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("API.Entities.PlaylistTrack", b =>
                {
                    b.Property<int>("TrackId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlaylistId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("TrackId", "PlaylistId");

                    b.HasIndex("CreatedById");

                    b.ToTable("PlaylistTrack");
                });

            modelBuilder.Entity("API.Entities.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChannelTitle")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("LargeThumbnail")
                        .HasColumnType("TEXT");

                    b.Property<string>("MediumThumbnail")
                        .HasColumnType("TEXT");

                    b.Property<string>("SmallThumbnail")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("YoutubeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("API.Entities.TrackLike", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrackId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Liked")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackLikes");
                });

            modelBuilder.Entity("API.Entities.AppUserTrack", b =>
                {
                    b.HasOne("API.Entities.AppUser", "User")
                        .WithMany("Tracks")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Track", "Track")
                        .WithMany("AppUsers")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.PlaylistTrack", b =>
                {
                    b.HasOne("API.Entities.AppUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("API.Entities.Playlist", "Playlist")
                        .WithMany("Tracks")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Track", "Track")
                        .WithMany("Playlists")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Playlist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("API.Entities.TrackLike", b =>
                {
                    b.HasOne("API.Entities.Track", "Track")
                        .WithMany("Likes")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("API.Entities.Playlist", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("API.Entities.Track", b =>
                {
                    b.Navigation("AppUsers");

                    b.Navigation("Likes");

                    b.Navigation("Playlists");
                });
#pragma warning restore 612, 618
        }
    }
}
