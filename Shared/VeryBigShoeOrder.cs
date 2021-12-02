using System.Xml.Serialization;

namespace Shared;

[Serializable()]
public class VeryBigShoeOrder
{
    [XmlAttribute(nameof(CustomerName))]
    public string? CustomerName { get; set; }

    [XmlAttribute(nameof(CustomerEmail))]
    public string? CustomerEmail { get; set; }

    [XmlAttribute(nameof(Quantity))]
    public int Quantity { get; set; }

    [XmlAttribute(nameof(Notes))]
    public string? Notes { get; set; }

    [XmlAttribute(nameof(Size))]
    public double Size { get; set; }

    [XmlAttribute(nameof(DateRequired))]
    public DateTime DateRequired { get; set; }
}

[Serializable]
[XmlRoot("BigShoeDataImport")]
public class BigShoeDataImport
{
    [XmlElement("BigShoeOrder")]
    public VeryBigShoeOrder[]? BigShoeOrders { get; set; }
}