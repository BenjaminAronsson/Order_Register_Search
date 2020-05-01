using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace xml_presenter_webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
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

            return cust;
        }
    }
}
