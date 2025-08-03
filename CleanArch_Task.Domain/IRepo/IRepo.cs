using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArch_Task.Domain.Entities;

namespace CleanArch_Task.Domain.IRepo
{
    public interface IRepo
    {
        // Event CRUD
        List<Event> GetAllEvent();
        Event? GetEventById(int id);
        Event CreateEvent(Event eventEntity);
        Event? UpdateEvent(int id, Event eventEntity);
        bool DeleteEvent(int id);

        // Order CRUD
        List<Order> GetAllOrder();
        Order? GetOrderById(int id);
        Order CreateOrder(Order order);
        Order? UpdateOrder(int id, Order order);
        bool DeleteOrder(int id);
    }
}