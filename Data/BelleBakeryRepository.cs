using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RebeccaResources.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebeccaResources.Data
{
    public class BelleBakeryRepository : IBelleBakeryRepository
    {
        private readonly BelleBakeryContext ctx;
        private readonly ILogger<BelleBakeryRepository> logger;

        public BelleBakeryRepository(BelleBakeryContext ctx, ILogger<BelleBakeryRepository> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public void AddEntity(object model)
        {
            ctx.Add(model);
        }

        public void AddOrder(Order newOrder)
        {
            // convert new products to lookup of product
            foreach (var item in newOrder.Items)
            {
                item.Product = ctx.Products.Find(item.Product.Id);
            }
            AddEntity(newOrder);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
            }
            else
            {
                return ctx.Orders
                .ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return ctx.Orders
                .Where( o => o.User.UserName == username)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
            }
            else
            {
                return ctx.Orders
                 .Where(o => o.User.UserName == username)
                .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                logger.LogInformation("Get all products was called.");
                return ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
           
        }

        public Order GetOrderById(string username, int id)
        {
            return ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(x => x.Id == id && x.User.UserName == username)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public bool SaveAll()
        {
            return ctx.SaveChanges() > 0;
        }
    }
}
