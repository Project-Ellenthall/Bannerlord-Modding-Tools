using System.Collections.Generic;
using System.Xml.Linq;

namespace TaleWorldsXMLParser.Framework
{
    public interface IXMLObject<XMLElements> where XMLElements : System.Enum
    {
        string Id { get; set; }
        Dictionary<XMLElements, Framework.IXMLCollection> Elements { get; set; }
        Dictionary<string, string> Attributes { get; set; }

        XElement Save();
    }
}
