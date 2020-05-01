using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace xml_presenter_webapp
{

    public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}

    [Serializable]
    public class OrderDTO {

        public OrderDTO() {
            this.Orders = new List<Order>();
        }

        [DataMember]
        public IEnumerable<Order> Orders { get; set; }

    }


    [Serializable]
    public class Order
    { 
        [DataMember]
        public string OrderNumber { get; set; }
        [DataMember]
        public string OrderLineNumber { get; set; }
        [DataMember]
        public string ProductNumber { get; set; }
        [DataMember]
        public string Quantity { get; set; }
        [DataMember]
        public string Summary { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string OrderDate { get; set; }
        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public string ProductGroup { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string CustomerNumber { get; set; }
    }   
}                