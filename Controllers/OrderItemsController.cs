using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RebeccaResources.Data;
using RebeccaResources.Data.Entities;
using RebeccaResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebeccaResources.Controllers
{
    [Route("api/order/{orderId}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly IBelleBakeryRepository repository;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;
        public OrderItemsController(IBelleBakeryRepository repository, ILogger<OrderItemsController> logger,
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
                var order = repository.GetOrderById(User.Identity.Name, orderId);
                if(order!= null) return Ok(mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items)); 
                return NotFound();
            
        }

        [HttpGet("{orderItemId}")]
        public IActionResult Get(int orderId, int orderItemId)
        {
            var order = repository.GetOrderById(User.Identity.Name, orderItemId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == orderItemId).FirstOrDefault();
                if (item != null)
                {
                    return Ok(mapper.Map<OrderItem, OrderItemViewModel>(item));
                }              
            }
           
            return NotFound();

        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderItemViewModel model)
        {
            //add it to the db
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrderItem = mapper.Map<OrderItemViewModel, OrderItem>(model);

                    newOrderItem.Order = repository.GetAllOrders(true).FirstOrDefault();

                    repository.AddEntity(newOrderItem);
                    if (repository.SaveAll())
                    {
                        var vm = mapper.Map<OrderItem, OrderItemViewModel>(newOrderItem);
                        //vm.Order.Id = repository.GetAllOrders(true).LastOrDefault().Id;
                        return Created($"/api/order/{newOrderItem.Order.Id}/items/{vm.OrderItemId}", vm);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save a new order item: {ex}.");
            }
            return BadRequest("Failed to save new order item");
        }
    }
}

