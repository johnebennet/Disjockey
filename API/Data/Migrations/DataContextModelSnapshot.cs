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

            modelBuilder.Entity("DisJockey.Core.AppUser", b =>
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

            modelBuilder.Entity("DisJockey.Core.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YoutubeId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("DisJockey.Core.PlaylistTrack", b =>
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

                    b.HasIndex("PlaylistId");

                    b.ToTable("PlaylistTracks");
                });

            modelBuilder.Entity("DisJockey.Core.PullUp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("TimePulled")
                        .HasColumnType("float");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.HasIndex("UserId");

                    b.ToTable("PullUps");
                });

            modelBuilder.Entity("DisJockey.Core.Track", b =>
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

            modelBuilder.Entity("DisJockey.Core.TrackLike", b =>
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

            modelBuilder.Entity("DisJockey.Core.TrackPlay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastPlayed")
                        .HasColumnType("datetime2");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackPlays");
                });

            modelBuilder.Entity("DisJockey.Core.TrackPlayHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TrackPlayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrackPlayId");

                    b.ToTable("TrackPlayHistory");
                });

            modelBuilder.Entity("DisJockey.Core.Playlist", b =>
                {
                    b.HasOne("DisJockey.Core.AppUser", null)
                        .WithMany("Playlists")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("DisJockey.Core.PlaylistTrack", b =>
                {
                    b.HasOne("DisJockey.Core.AppUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("DisJockey.Core.Playlist", "Playlist")
                        .WithMany("Tracks")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DisJockey.Core.Track", "Track")
                        .WithMany("Playlists")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Playlist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("DisJockey.Core.PullUp", b =>
                {
                    b.HasOne("DisJockey.Core.Track", "Track")
                        .WithMany("PullUps")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DisJockey.Core.AppUser", "User")
                        .WithMany("PullUps")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DisJockey.Core.TrackLike", b =>
                {
                    b.HasOne("DisJockey.Core.Track", "Track")
                        .WithMany("Likes")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DisJockey.Core.AppUser", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DisJockey.Core.TrackPlay", b =>
                {
                    b.HasOne("DisJockey.Core.AppUser", "User")
                        .WithMany("Tracks")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DisJockey.Core.Track", "Track")
                        .WithMany("TrackPlays")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DisJockey.Core.TrackPlayHistory", b =>
                {
                    b.HasOne("DisJockey.Core.TrackPlay", "TrackPlay")
                        .WithMany("TrackPlayHistory")
                        .HasForeignKey("TrackPlayId");

                    b.Navigation("TrackPlay");
                });

            modelBuilder.Entity("DisJockey.Core.AppUser", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("Playlists");

                    b.Navigation("PullUps");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("DisJockey.Core.Playlist", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("DisJockey.Core.Track", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("Playlists");

                    b.Navigation("PullUps");

                    b.Navigation("TrackPlays");
                });

            modelBuilder.Entity("DisJockey.Core.TrackPlay", b =>
                {
                    b.Navigation("TrackPlayHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
