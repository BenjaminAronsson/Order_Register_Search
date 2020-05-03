using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace xml_presenter_webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
            OrderRepository.generateXml();
        }
     
        

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            
            var orderList = OrderRepository.GetAllOrders();
            return orderList;
        }
    
        [HttpGet("orderid/{orderId}")]
        public OrderDTO GetOrder([FromRoute]string orderId) {
          var order = OrderRepository.GetOrderWithId(orderId);

          if (order != null) {
              return order;
          } else {
              return null;
          }
        }
    }

}
