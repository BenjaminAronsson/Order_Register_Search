using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace xml_presenter_webapp
{
    public class OrderDTO
    { 
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }

        public List<Product> Products { get; set;}

        public OrderDTO() {
            this.Products = new List<Product>();
        }
    }   
}                