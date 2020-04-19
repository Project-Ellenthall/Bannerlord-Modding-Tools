using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaleWorldsXMLParser.Framework
{
    public interface IXMLParser<XMLElements> where XMLElements : System.Enum
    {
        Dictionary<string, string> Attributes { get; }
        Dictionary<XMLElements, ICollection> Elements { get; }

        void Load(string filepath);

        void Update(Dictionary<string, Dictionary<string, string>> data);

        void Save(string filepath);
    }
}
