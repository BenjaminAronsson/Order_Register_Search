﻿using System;
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
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
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
            string[] modifiedSource = source.Skip(8).ToArray();
            XElement cust = new System.Xml.Linq.XElement("Orders",  
                from str in source  
                let fields = str.Split('|')  
                select new XElement("Order",  
                    new XAttribute("OrderNumber", fields[1]),  
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


            //save xml to disk
            const string filePath = "db/orders.xml";
            cust.Save(filePath);

            //var orders = Serialization<OrderDTO>.DeserializeFromXmlFile(filePath);

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



           // System.Console.WriteLine(xmlstr);

           // var xmlString = System.IO.File.ReadAllLines(filePath);

            //string xmlString = "<Products><Product><Id>1</Id><Name>My XML product</Name></Product><Product><Id>2</Id><Name>My second product</Name></Product></Products>";

            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>), new XmlRootAttribute("Orders"));

            StringReader stringReader = new StringReader(xmlString);

            List<Order> productList = (List<Order>)serializer.Deserialize(stringReader);


            foreach(Order o in productList.ToArray()) {
                System.Console.WriteLine(o.CustomerName);
            }

            /* var node = cust.Elements().First(); */

/*              (order)deserializer.ReadObject(cust.de);/ */

/*     Serialization<Order>.DeserializeFromXmlFile(yourFileNameOrPath); */
            
            
/*              XmlNode xmlNode = cust.Elements().First();

 XmlSerializer serial = new XmlSerializer(typeof(SystemInfo));

 SystemInfo syso =(SystemInfo)serial.Deserialize(new X mlNodeReader(xmlNode)); */

            return new List<Order>();
        }
    }
}
