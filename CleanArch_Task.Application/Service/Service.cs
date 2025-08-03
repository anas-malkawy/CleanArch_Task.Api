using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArch_Task.Domain.Entities;
using CleanArch_Task.Domain.IRepo;
using CleanArch_Task.Application.DTOs;
using CleanArch_Task.Application.IService;

namespace CleanArch_Task.Application.Service
{
    public class Service : CleanArch_Task.Application.IService.IService
    {
        private readonly IRepo repo;

        public Service(IRepo _repo)
        {
            repo = _repo;
        }

        // Event CRUD Implementation
        public List<Event> GetAllEvent()
        {
            var events = repo.GetAllEvent();
            return events;
        }


        public Event? GetEventById(int id)
        {
            var eventEntity = repo.GetEventById(id);
            if (eventEntity == null) return null;

            return eventEntity;
        }

        public EventDTO CreateEvent(EventDTO eventDto)
        {
            var eventEntity = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                VenueId = eventDto.VenueId
            };

            var created = repo.CreateEvent(eventEntity);

            return new EventDTO
            {
                Title = created.Title,
                Description = created.Description,
                VenueId = created.VenueId
            };
        }

        public EventDTO? UpdateEvent(int id, EventDTO eventDto)
        {
            var eventEntity = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                VenueId = eventDto.VenueId
            };

            var updated = repo.UpdateEvent(id, eventEntity);
            if (updated == null) return null;

            return new EventDTO
            {
                Title = updated.Title,
                Description = updated.Description,
                VenueId = updated.VenueId
            };
        }

        public bool DeleteEvent(int id)
        {
            return repo.DeleteEvent(id);
        }

        // Order CRUD Implementation
        public List<OrderDTO> GetAllOrder()
        {
            var orders = repo.GetAllOrder();
            return orders.Select(o => new OrderDTO
            {
                EventId = o.EventId,
                CustomerId = o.CustomerId,
                Quantity = o.Quantity,
                UnitPrice = o.UnitPrice
            }).ToList();
        }

        public OrderDTO? GetOrderById(int id)
        {
            var order = repo.GetOrderById(id);
            if (order == null) return null;

            return new OrderDTO
            {
                EventId = order.EventId,
                CustomerId = order.CustomerId,
                Quantity = order.Quantity,
                UnitPrice = order.UnitPrice
            };
        }

        public OrderDTO CreateOrder(OrderDTO orderDto)
        {
            var order = new Order
            {
                EventId = orderDto.EventId,
                CustomerId = orderDto.CustomerId,
                Quantity = orderDto.Quantity,
                UnitPrice = orderDto.UnitPrice
            };

            var created = repo.CreateOrder(order);

            return new OrderDTO
            {
                EventId = created.EventId,
                CustomerId = created.CustomerId,
                Quantity = created.Quantity,
                UnitPrice = created.UnitPrice
            };
        }

        public OrderDTO? UpdateOrder(int id, OrderDTO orderDto)
        {
            var order = new Order
            {
                EventId = orderDto.EventId,
                CustomerId = orderDto.CustomerId,
                Quantity = orderDto.Quantity,
                UnitPrice = orderDto.UnitPrice
            };

            var updated = repo.UpdateOrder(id, order);
            if (updated == null) return null;

            return new OrderDTO
            {
                EventId = updated.EventId,
                CustomerId = updated.CustomerId,
                Quantity = updated.Quantity,
                UnitPrice = updated.UnitPrice
            };
        }

        public bool DeleteOrder(int id)
        {
            return repo.DeleteOrder(id);
        }
    }
}
