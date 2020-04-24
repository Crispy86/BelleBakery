using RebeccaResources.Data.Entities;
using System.Collections.Generic;

namespace RebeccaResources.Data
{
    public interface IBelleBakeryRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);      
        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderById(string username, int id);
        bool SaveAll();
        void AddEntity(object model);
        void AddOrder(Order newOrder);
    }
}