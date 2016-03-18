using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SageIntegration;
using System.IO;
using DateUtils;

namespace WebsiteXMLImport
{
    [XmlRoot("Orders")]
    public class Orders
    {
        public Orders() { Items = new List<Order>(); }
        [XmlElement("Order")]
        public List<Order> Items { get; set; }


        public static SO_OrderBatch ReadXML(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Orders));
            Orders oOrders = new Orders();

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                oOrders = (Orders)serializer.Deserialize(fs);
            }

            SO_OrderBatch batch = new SO_OrderBatch();

            foreach (Order xmlorder in oOrders.Items)
            {
                SalesOrder order = new SalesOrder();
                order.RequiredDate = DateUtils.DateUtils.ShipDate(DateTime.Today, 5);

                order.ShipToName = xmlorder.ShippingFullName;
                order.ShipToAddress1 = xmlorder.ShippingAddress1;
                order.ShipToAddress2 = xmlorder.ShippingAddress2;
                order.ShipToZipcode = xmlorder.ShippingPostCode;

                order.CustomerNo = xmlorder.Customer_Number;
                order.CustomerPONo = "Website-" + xmlorder.OrderId;
                
                foreach (OrderLineItems xmlline in xmlorder.Items)
                {
                    order.AddLine(new LineItem(xmlline.SKU, xmlline.Quantity, xmlline.Meta));
                }

                batch.AddOrder(order);
            }

            return batch;


        }
    }
}
