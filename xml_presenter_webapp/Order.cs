using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace xml_presenter_webapp
{
    public class Order
    { 
        public string OrderNumber { get; set; }
        public string OrderLineNumber { get; set; }
        public string ProductNumber { get; set; }
        public string Quantity { get; set; }
        public string Summary { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OrderDate { get; set; }
        public string Price { get; set; }
        public string ProductGroup { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
    }   
}                