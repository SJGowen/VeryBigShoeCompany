using Newtonsoft.Json;
using Shared;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DataUploadValidation;

static class Program
{
    static void Main(string[] args)
    {
        var xml = LoadXMLFile("OrderImport.xsd", "test.xml");

        IEnumerable<VeryBigShoeOrder> orders = GetOrdersFromXML(xml);
        
        OrderValidator validator = new();
        foreach (var order in orders)
        {
            string errors = validator.Validate(order);
            if (errors != "")
                Console.WriteLine(errors);
        }

        string jsonText = JsonConvert.SerializeObject(orders);
        Console.WriteLine(jsonText);
        
    }

    private static IEnumerable<VeryBigShoeOrder> GetOrdersFromXML(XDocument doc)
    {
        XmlSerializer serializer = new(typeof(BigShoeDataImport));

        BigShoeDataImport? data = serializer.Deserialize(doc.Root.CreateReader()) as BigShoeDataImport;

        return data.Order.Select(d => new VeryBigShoeOrder
        {
            CustomerName = d.CustomerName,
            CustomerEmail = d.CustomerEmail,
            Quantity = d.Quantity,
            Notes = d.Notes,
            Size = d.Size,
            DateRequired = d.DateRequired
        }).ToList();
    }

    private static XDocument LoadXMLFile(string xsd, string filename)
    {
        XmlSchemaSet schemas = new();
        schemas.Add("", xsd);

        XDocument xDoc = XDocument.Load(filename);
        //bool errors = false;
        xDoc.Validate(schemas, (o, e) =>
        {
            Console.WriteLine($"{e.Message}");
            //errors = true;
        });
        //Console.WriteLine("Document {0}", errors ? "did not load" : "loaded");

        return xDoc;
    }
}
