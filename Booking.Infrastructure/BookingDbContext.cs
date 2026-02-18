using Booking.Domain.Addresses;
using Booking.Domain.Bookings;
using Booking.Domain.OwnerProfiles;
using Booking.Domain.Properties;
using Booking.Domain.Reviews;
using Booking.Domain.Roles;
using Booking.Domain.UserRoles;
using Booking.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure;

public class BookingDbContext : DbContext {

    public BookingDbContext() { }

    public BookingDbContext (DbContextOptions<BookingDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; private set; }
    public DbSet<Role> Roles { get; private set; }
    public DbSet<UserRole> UserRoles { get; private set; } 
    public DbSet<OwnerProfile> OwnerProfiles { get; private set; }
    public DbSet<Property> Properties { get; private set; }
    public DbSet<Address> Addresses { get; private set; }
    public DbSet<BookingEntity> Bookings { get; private set; }
    public DbSet<Review> Reviews { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<User>(entity => {

            entity.HasKey(u => u.Id);

            entity.HasIndex(u => u.Email)
                .IsUnique();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.Id);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(ur
                => new { ur.UserId, ur.RoleId });

            entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        });

        modelBuilder.Entity<OwnerProfile>(entity =>
        {
            entity.HasKey(op => op.UserId);

            entity.HasOne(op => op.User)
                .WithOne(u => u.OwnerProfile)
                .HasForeignKey<OwnerProfile>(op => op.UserId);
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasOne(p => p.Owner)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.OwnerId);

            entity.HasOne(p => p.Address)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AddressId);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(p => p.Id);
        });

        modelBuilder.Entity<BookingEntity>(entity =>
        {
            entity.HasKey(b => b.Id);

            entity.Property(b => b.CleaningFee).HasPrecision(18, 2);
            entity.Property(b => b.AmenitiesUpCharge).HasPrecision(18, 2);
            entity.Property(b => b.PriceForPeriod).HasPrecision(18, 2);
            entity.Property(b => b.TotalPrice).HasPrecision(18, 2);

            entity.HasOne(b => b.Property)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.Guest)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.GuestId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasIndex(r => r.BookingId)
                .IsUnique();

            entity.Property(r => r.Rating)
                .HasPrecision(2, 1);

            entity.HasOne(r => r.Booking)
                .WithOne(b => b.Review)
                .HasForeignKey<Review>(r => r.BookingId);

            entity.HasOne(r => r.Guest)
                .WithMany(g => g.Reviews)
                .HasForeignKey(r => r.GuestId);
        });
    }
}