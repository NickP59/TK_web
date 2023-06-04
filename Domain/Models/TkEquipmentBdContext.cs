using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace tk_web.Domain.Models;

public partial class TkEquipmentBdContext : DbContext
{
    public TkEquipmentBdContext()
    {
    }

    public TkEquipmentBdContext(DbContextOptions<TkEquipmentBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<EquipmentPlace> EquipmentPlaces { get; set; }

    public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Organizer> Organizers { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Position_> Positions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build().GetConnectionString("connection_string"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.HandoverDate)
                .HasColumnType("date")
                .HasColumnName("handover_date");
            entity.Property(e => e.IsuueDate)
                .HasColumnType("date")
                .HasColumnName("isuue_date");
            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK_Booking_Equipment");

            entity.HasOne(d => d.Event).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Events_");

            entity.HasOne(d => d.Participant).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ParticipantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Participants");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Place).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipment_Equipment_place");

            entity.HasOne(d => d.Type).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipment_Equipment_type");
        });

        modelBuilder.Entity<EquipmentPlace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Equipment_palce");

            entity.ToTable("Equipment_place");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<EquipmentType>(entity =>
        {
            entity.ToTable("Equipment_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Events_");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountOfPeople).HasColumnName("count_of_people");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.SocialNetworkLink)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("social_network_link");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.SocialNetworkLink)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("social_network_link");

            entity.HasOne(d => d.Group).WithMany(p => p.Participants)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Participants_Groups");

            entity.HasOne(d => d.Position).WithMany(p => p.Participants)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_Participants_Positions");
        });

        modelBuilder.Entity<Position_>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
