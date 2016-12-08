using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Assignment_1_DRAFT
{
    partial class ShoppingCart
    {

        public void FileRead()
        {
            try
            {
                var doc = XDocument.Load("product.inventory.xml");
                var results = from c in doc.Descendants("product")
                              select c;

                foreach (var contact in results)
                {
                    //temp store in local variables and any necessary data converted
                    recNo = int.Parse(contact.Element("recordNumber").Value);
                    name = contact.Element("name").Value;
                    stock = int.Parse(contact.Element("stock").Value);
                    price = int.Parse(contact.Element("price").Value);
                    //Create new obj ref using Product class
                    catalog.Add(new Product()
                    {
                        ID = recNo,
                        prodName = name,
                        price = price,
                        stock = stock
                    });
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
            }

        }

        public void UpdateXML()
        {

            XmlTextWriter xWriter = new XmlTextWriter("product.inventory.xml", Encoding.UTF8);
            xWriter.Formatting = Formatting.Indented;
            xWriter.WriteStartElement("inventory");

            //loop through catalog array and rewrite all elements to a new xml file. file would
            // be overriden or a new xml file will be created.
            for (int i = 0; i < catalog.Count; i++)
            {
                xWriter.WriteStartElement("product");
                xWriter.WriteStartElement("recordNumber");
                xWriter.WriteString(catalog[i].ID.ToString());
                xWriter.WriteEndElement();
                xWriter.WriteStartElement("name");
                xWriter.WriteString(catalog[i].prodName); 
                xWriter.WriteEndElement();
                xWriter.WriteStartElement("stock");
                xWriter.WriteString(catalog[i].stock.ToString()); 
                xWriter.WriteEndElement();
                xWriter.WriteStartElement("price");
                xWriter.WriteString(catalog[i].price.ToString()); 
                xWriter.WriteEndElement();
                xWriter.WriteEndElement();
            }
            xWriter.WriteEndElement();
            xWriter.Close();
        }
    }
}
