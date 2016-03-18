using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebsiteXMLImport
{
    public class Order
    {
        public int OrderId { get; set; }
        int OrderNumber { get; set; }
        public string Customer_Number { get; set; }
        public string OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string BillingFullName { get; set; }
        public string BillingCompany { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingPostCode { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPhone { get; set; }
        public string BillingEmail { get; set; }
        public string ShippingFirstName { get; set; }
        public string ShippingLastName { get; set; }
        public string ShippingFullName { get; set; }
        public string ShippingCompany { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPostCode { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingMethodId { get; set; }
        public string ShippingMethod { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal OrderDiscountTotal { get; set; }
        public decimal CartDiscountTotal { get; set; }
        public decimal ShippingTotal { get; set; }
        public decimal ShippingTaxTotal { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal FeeTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public string CompletedDate { get; set; }
        public string CustomerNote { get; set; }
        public int CustomerId { get; set; }

        public Order() { Items = new List<OrderLineItems>(); }
        [XmlElement("OrderLineItems")]
        public List<OrderLineItems> Items { get; set; }
    }
}
