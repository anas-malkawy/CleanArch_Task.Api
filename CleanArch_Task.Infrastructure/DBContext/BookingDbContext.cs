using CleanArch_Task.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch_Task.Infrastructure.DBContext
{
    public partial class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for Price and UnitPrice
            modelBuilder.Entity<Ticket>()
                .Property(t => t.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.UnitPrice)
                .HasPrecision(18, 2);

            // Seed Tenants
            modelBuilder.Entity<Tenant>()
                .HasData(
                    new Tenant { Id = 1, Email = "contact@eventcorp.com", Name = "EventCorp Solutions" },
                    new Tenant { Id = 2, Email = "info@premierent.com", Name = "Premier Entertainment" },
                    new Tenant { Id = 3, Email = "hello@globalevents.com", Name = "Global Events Management" }
                );

            // Seed Venues (1 per tenant)
            modelBuilder.Entity<Venue>()
                .HasData(
                    new Venue { Id = 1, Name = "Grand Convention Center", Location = "Downtown Plaza, City Center", TenantId = 1 },
                    new Venue { Id = 2, Name = "Riverside Amphitheater", Location = "Waterfront District, West Side", TenantId = 2 },
                    new Venue { Id = 3, Name = "Metropolitan Arena", Location = "Sports Complex, North District", TenantId = 3 }
                );

            // Seed Events (2 per venue)
            modelBuilder.Entity<Event>()
                .HasData(
                    // EventCorp Solutions Events
                    new Event { Id = 1, Title = "Tech Innovation Summit 2025", Description = "Annual technology conference featuring industry leaders", VenueId = 1 },
                    new Event { Id = 2, Title = "Business Leadership Forum", Description = "Executive networking event with keynote speakers", VenueId = 1 },

                    // Premier Entertainment Events
                    new Event { Id = 3, Title = "Summer Music Festival", Description = "Three-day outdoor music festival", VenueId = 2 },
                    new Event { Id = 4, Title = "Comedy Night Spectacular", Description = "Stand-up comedy show with top comedians", VenueId = 2 },

                    // Global Events Management Events
                    new Event { Id = 5, Title = "International Trade Expo", Description = "Global marketplace connecting businesses", VenueId = 3 },
                    new Event { Id = 6, Title = "Sports Championship Finals", Description = "Championship finals with premium viewing", VenueId = 3 }
                );

            // Seed Customers (3 test customers)
            modelBuilder.Entity<Customer>()
                .HasData(
                    new Customer { Id = 1, FullName = "Ahmed Mohammed", Email = "ahmed.mohammed@email.com" },
                    new Customer { Id = 2, FullName = "Sarah Johnson", Email = "sarah.johnson@email.com" },
                    new Customer { Id = 3, FullName = "Omar Al-Rashid", Email = "omar.alrashid@email.com" }
                );

            // Seed Tickets (50 per event)
            var tickets = new List<Ticket>();
            int ticketId = 1;

            // Event 1 - Tech Summit (50 tickets)
            for (int i = 1; i <= 10; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "VIP", Price = 500.00m, QuantityAvailable = 1, IsVip = true });
            for (int i = 1; i <= 15; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Premium", Price = 250.00m, QuantityAvailable = 1, IsVip = false });
            for (int i = 1; i <= 25; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Standard", Price = 100.00m, QuantityAvailable = 1, IsVip = false });

            // Event 2 - Business Forum (50 tickets)
            for (int i = 1; i <= 10; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "VIP", Price = 400.00m, QuantityAvailable = 1, IsVip = true });
            for (int i = 1; i <= 15; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Premium", Price = 200.00m, QuantityAvailable = 1, IsVip = false });
            for (int i = 1; i <= 25; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Standard", Price = 80.00m, QuantityAvailable = 1, IsVip = false });

            // Event 3 - Music Festival (50 tickets)
            for (int i = 1; i <= 10; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "VIP", Price = 300.00m, QuantityAvailable = 1, IsVip = true });
            for (int i = 1; i <= 15; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Premium", Price = 150.00m, QuantityAvailable = 1, IsVip = false });
            for (int i = 1; i <= 25; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Standard", Price = 75.00m, QuantityAvailable = 1, IsVip = false });

            // Event 4 - Comedy Night (50 tickets)
            for (int i = 1; i <= 10; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "VIP", Price = 200.00m, QuantityAvailable = 1, IsVip = true });
            for (int i = 1; i <= 15; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Premium", Price = 100.00m, QuantityAvailable = 1, IsVip = false });
            for (int i = 1; i <= 25; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Standard", Price = 50.00m, QuantityAvailable = 1, IsVip = false });

            // Event 5 - Trade Expo (50 tickets)
            for (int i = 1; i <= 10; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "VIP", Price = 450.00m, QuantityAvailable = 1, IsVip = true });
            for (int i = 1; i <= 15; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Premium", Price = 225.00m, QuantityAvailable = 1, IsVip = false });
            for (int i = 1; i <= 25; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Standard", Price = 90.00m, QuantityAvailable = 1, IsVip = false });

            // Event 6 - Sports Finals (50 tickets)
            for (int i = 1; i <= 10; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "VIP", Price = 600.00m, QuantityAvailable = 1, IsVip = true });
            for (int i = 1; i <= 15; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Premium", Price = 300.00m, QuantityAvailable = 1, IsVip = false });
            for (int i = 1; i <= 25; i++) tickets.Add(new Ticket { Id = ticketId++, Type = "Standard", Price = 120.00m, QuantityAvailable = 1, IsVip = false });

            modelBuilder.Entity<Ticket>()
                .HasData(tickets.ToArray());

            // Seed Sample Orders (using static dates instead of DateTime.UtcNow)
            modelBuilder.Entity<Order>()
                .HasData(
                    new Order { Id = 1, EventId = 1, CustomerId = 1, Quantity = 2, UnitPrice = 150.00m, CreatedAt = new DateTime(2025, 7, 27, 10, 0, 0, DateTimeKind.Utc) },
                    new Order { Id = 2, EventId = 3, CustomerId = 2, Quantity = 1, UnitPrice = 300.00m, CreatedAt = new DateTime(2025, 7, 29, 14, 30, 0, DateTimeKind.Utc) },
                    new Order { Id = 3, EventId = 5, CustomerId = 3, Quantity = 3, UnitPrice = 100.00m, CreatedAt = new DateTime(2025, 7, 31, 9, 15, 0, DateTimeKind.Utc) },
                    new Order { Id = 4, EventId = 2, CustomerId = 1, Quantity = 1, UnitPrice = 500.00m, CreatedAt = new DateTime(2025, 8, 1, 16, 45, 0, DateTimeKind.Utc) },
                    new Order { Id = 5, EventId = 4, CustomerId = 2, Quantity = 2, UnitPrice = 75.00m, CreatedAt = new DateTime(2025, 8, 2, 11, 20, 0, DateTimeKind.Utc) }
                );
        }
    }
}