using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArch_Task.Domain.Entities;
using CleanArch_Task.Domain.IRepo;
using CleanArch_Task.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArch_Task.Infrastructure.Repo
{
    public class Repo : IRepo
    {
        private readonly BookingDbContext context;

        public Repo(BookingDbContext _context)
        {
            context = _context;
        }

        // Event CRUD Implementation
        public List<Event> GetAllEvent()
        {
            return context.Events.AsNoTracking().ToList();
        }

        public Event? GetEventById(int id)
        {
            return context.Events.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public Event CreateEvent(Event eventEntity)
        {
            context.Events.Add(eventEntity);
            context.SaveChanges();
            return eventEntity;
        }

        public Event? UpdateEvent(int id, Event eventEntity)
        {
            var existing = context.Events.Find(id);
            if (existing == null) return null;

            existing.Title = eventEntity.Title;
            existing.Description = eventEntity.Description;
            existing.VenueId = eventEntity.VenueId;

            context.SaveChanges();
            return existing;
        }

        public bool DeleteEvent(int id)
        {
            var eventEntity = context.Events.Find(id);
            if (eventEntity == null) return false;

            context.Events.Remove(eventEntity);
            context.SaveChanges();
            return true;
        }

        // Order CRUD Implementation
        public List<Order> GetAllOrder()
        {
            return context.Orders.AsNoTracking().ToList();
        }

        public Order? GetOrderById(int id)
        {
            return context.Orders.AsNoTracking().FirstOrDefault(o => o.Id == id);
        }

        public Order CreateOrder(Order order)
        {
            order.CreatedAt = DateTime.UtcNow;
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }

        public Order? UpdateOrder(int id, Order order)
        {
            var existing = context.Orders.Find(id);
            if (existing == null) return null;

            existing.EventId = order.EventId;
            existing.CustomerId = order.CustomerId;
            existing.Quantity = order.Quantity;
            existing.UnitPrice = order.UnitPrice;

            context.SaveChanges();
            return existing;
        }

        public bool DeleteOrder(int id)
        {
            var order = context.Orders.Find(id);
            if (order == null) return false;

            context.Orders.Remove(order);
            context.SaveChanges();
            return true;
        }
    }
}
