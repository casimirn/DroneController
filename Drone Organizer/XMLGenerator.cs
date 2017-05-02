using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;


namespace Drone_Organizer
{
    class XMLGenerator
    {
        private static Dictionary<int, List<Drone>> frames;

        public static void init()
        {
            frames = new Dictionary<int, List<Drone>>();
        }

        public static void StoreFrame(int frameNumber, Drone[,] drones, int rows, int cols)
        {
            List<Drone> DroneList = new List<Drone>();

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    DroneList.Add(drones[r, c]);

            if (frames.ContainsKey(frameNumber))
                frames.Remove(frameNumber);
            frames.Add(frameNumber, DroneList);
        }

        public static void CreateXml()
        {
            if (frames == null)
                return;

            var list = frames.Keys.ToList();
            list.Sort();

            /*XmlSerializer ser = new XmlSerializer(typeof(XmlElement));
            XmlElement droneArray = new XmlDocument().CreateElement("DroneArray", "ns");
            droneArray.InnerText = "Drones";*/
            //TextWriter writer = new StreamWriter("DronePath.xml");


            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode productsNode = doc.CreateElement("products");
            doc.AppendChild(productsNode);

            XmlNode productNode = doc.CreateElement("product");
            XmlAttribute productAttribute = doc.CreateAttribute("id");
            productAttribute.Value = "01";
            productNode.Attributes.Append(productAttribute);
            productsNode.AppendChild(productNode);

            XmlNode nameNode = doc.CreateElement("Name");
            nameNode.AppendChild(doc.CreateTextNode("Java"));
            productNode.AppendChild(nameNode);
            XmlNode priceNode = doc.CreateElement("Price");
            priceNode.AppendChild(doc.CreateTextNode("Free"));
            productNode.AppendChild(priceNode);

            // Create and add another product node.
            productNode = doc.CreateElement("product");
            productAttribute = doc.CreateAttribute("id");
            productAttribute.Value = "02";
            productNode.Attributes.Append(productAttribute);
            productsNode.AppendChild(productNode);
            nameNode = doc.CreateElement("Name");
            nameNode.AppendChild(doc.CreateTextNode("C#"));
            productNode.AppendChild(nameNode);
            priceNode = doc.CreateElement("Price");
            priceNode.AppendChild(doc.CreateTextNode("Free"));
            productNode.AppendChild(priceNode);

            doc.Save("Drone.xml");

            /*foreach (var key in list)
            {
                droneArray.
            }

            ser.Serialize(writer, droneArray);
            writer.Close();*/
        }

        /*public class DroneXml:ICollection
        {

        }*/
    }
}
