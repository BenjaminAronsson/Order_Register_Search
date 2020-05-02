using System.Collections.Concurrent;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
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
            generateXml();
        }
        const string filePath = "db/orders.xml";
        private void generateXml() {
            

            //find files
            string[] txtfiles = Directory.GetFiles("db", "*.txt");
            
            // Read into an array of strings.  
            List<string> source = new List<string>();
            
            foreach(string file in txtfiles) { 
                //read file from disk
                var linesFromFile = System.IO.File.ReadAllLines(file);
                
                //remove table headers
                linesFromFile = linesFromFile.Skip(1).ToArray();
                //combine files
                source.AddRange(linesFromFile);  
            }

            //convert tp xml
            string[] modifiedSource = source.Skip(8).ToArray();
            XElement cust = new System.Xml.Linq.XElement("Orders",  
                from str in source  
                let fields = str.Split('|')  
                select new XElement("Order",  
                    new XElement("OrderNumber", fields[1]),  
                    new XElement("OrderLineNumber", fields[2]),  
                    new XElement("ProductNumber", fields[3]),  
                    new XElement("Quantity", fields[4]),  
                    new XElement("Name", fields[5]),  
                    new XElement("Description", fields[6]),  
                    new XElement("Price", fields[7]),  
                    new XElement("ProductGroup", fields[8]),  
                    new XElement("OrderDate", fields[9]),  
                    new XElement("CustomerName", fields[10]),  
                    new XElement("CustomerNumber", fields[11])
                )  
            );

            //avoid making duplicates
            if (!System.IO.File.Exists(filePath)) {
                 //save xml to disk
                cust.Save(filePath);
            }
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            
            //load xml from disk
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            //xml to string
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            doc.WriteTo(tx);

            //remove xml tag
            string xmlstr = sw.ToString();
            const int xmlTagLength = 38;
            string xmlString = xmlstr.Substring(xmlTagLength, xmlstr.Length-xmlTagLength);

            // create instances
            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>), new XmlRootAttribute("Orders"));
            StringReader stringReader = new StringReader(xmlString);
            List<Order> orderList = (List<Order>)serializer.Deserialize(stringReader);

            return orderList;
        }
    
        [HttpGet("orderid/{orderId}")]
        public OrderDTO GetOrder([FromRoute]string orderId) {
            var allOrders = this.Get();
            System.Console.WriteLine("fetching order: " + orderId);
            var selectedOrder = allOrders.Where((order) => order.OrderNumber == orderId);
    
            if(selectedOrder.Count() > 0) {
                var order = new OrderDTO();
                var info = selectedOrder.First();
                order.CustomerName = info.CustomerName;
                order.CustomerNumber = info.CustomerNumber;
                order.OrderNumber = info.OrderNumber;
                order.OrderDate = info.OrderDate;
                 
                System.Console.WriteLine(selectedOrder.Count());
                foreach(Order orderDTO in selectedOrder) {
                    var product = new Product();

                    product.Name = orderDTO.Name; 
                    product.OrderLineNumber = orderDTO.OrderLineNumber; 
                    product.Quantity = orderDTO.Quantity; 
                    product.ProductGroup = orderDTO.ProductGroup; 
                    product.ProductNumber = orderDTO.ProductNumber; 
                    product.Price = orderDTO.Price; 
                    product.Description = orderDTO.Description; 

                    order.Products.Add(product);
                }
                 

                System.Console.WriteLine(order.Products.Count());

                return order;
            } else {
                return null;
            }
        }
    }

}
