using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaleWorldsXMLParser.Culture
{
    public class CultureParser : Framework.IXMLParser<Element>
    {
        public Regex textRegex;
        public Dictionary<Element, Framework.ICollection> Elements { get; protected set; }
        public Dictionary<string, string> Attributes { get; protected set; }

        public CultureParser(string filepath)
        {
            Attributes = new Dictionary<string, string>();
            Elements = new Dictionary<Element, Framework.ICollection>();   
            
            for(Element e = Element.maleNames; e <= Element.clanNames; e++)
            {
                Elements.Add(e, e.NewCollection());
            }

            this.Load(filepath);
        }

        public void Load(string filepath)
        {
            XElement xmlDocument = XElement.Load(filepath);

            foreach (var attribute in xmlDocument.Attributes())
            {
                Attributes.Add(attribute.Name.ToString(), attribute.Value);  
            }

            foreach (var element in xmlDocument.Elements())
            {
                var actualElement = Elements[element.Name.ToString().ToElement()];

                foreach(var child in element.Elements())
                {
                    var text = new Framework.Text(child.Value);
                    actualElement.AddExisting(text.Id, text.Value);
                }
                
            }
        }

        public void Save(string filepath)
        {

        }

        public void Update(Dictionary<string, Dictionary<string, string>> data) => throw new NotImplementedException();
    }
}
