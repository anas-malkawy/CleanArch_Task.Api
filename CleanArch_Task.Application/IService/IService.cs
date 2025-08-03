using CleanArch_Task.Application.DTOs;
using CleanArch_Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch_Task.Application.IService
{
    public interface IService
    {
        // Event CRUD
        List<Event> GetAllEvent();
        Event? GetEventById(int id);
        EventDTO CreateEvent(EventDTO eventDto);
        EventDTO? UpdateEvent(int id, EventDTO eventDto);
        bool DeleteEvent(int id);

        // Order CRUD
        List<OrderDTO> GetAllOrder();
        OrderDTO? GetOrderById(int id);
        OrderDTO CreateOrder(OrderDTO orderDto);
        OrderDTO? UpdateOrder(int id, OrderDTO orderDto);
        bool DeleteOrder(int id);
    }
}