using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaleWorldsXMLParser.Culture
{
    public class Culture : Framework.IXMLObject<Element>
    {
        public const string XML_CULTURE = "Culture";

        public Dictionary<Element, Framework.IXMLCollection> Elements { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public string Id { get; set; }

        public Culture(XElement culture)
        {
            Attributes = new Dictionary<string, string>();
            Elements = new Dictionary<Element, Framework.IXMLCollection>();
            //initialize elements dictionary.
            for (Element e = Element.maleNames; e <= Element.clanNames; e++)
            {
                Elements.Add(e, e.NewCollection());
            }

            Id = culture.Attributes().ToList()[0].Value;

            //foreach attribute, add it to the attributes dictionary.
            foreach (var attribute in culture.Attributes())
            {
                Attributes.Add(attribute.Name.ToString(), attribute.Value);
            }

            //foreach name child, parse through each child and add it to the collection. 
            foreach (var element in culture.Elements())
            {
                var actualElement = Elements[element.Name.ToString().ToElement()];

                foreach (var child in element.Elements())
                {
                    var text = new Framework.Text(child.Attribute("name").Value);
                    actualElement.AddExisting(text.Id, text.Value);
                }

            }
        }
        /// <summary>
        /// Method to go through each xml object and attribute to save to this xml object.
        /// </summary>
        /// <returns>Returns an xml object for this structure. </returns>
        public XElement Save()
        {
            XElement culture = new XElement(XML_CULTURE);
            foreach(var attribute in Attributes)
            {
                culture.SetAttributeValue(attribute.Key, attribute.Value);
            }
            foreach(var element in Elements)
            {
                culture.Add(element.Value.Save());
            }
            return culture;
        }
    }
}
