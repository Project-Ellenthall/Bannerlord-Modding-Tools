using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaleWorldsXMLParser.Framework
{
    public struct Text
    {
        private static Regex idRegex = new Regex("{=(.+)}(.+)");

        public string Id { get; set; }
        public string Value { get; set; }

        public Text(string id, string value)
        {
            this.Id = id;
            this.Value = value;
        }

        public Text(string text)
        {
            var match = idRegex.Match(text);
            this.Id = match.Groups[0].Value;
            this.Value = match.Groups[1].Value;
        }
    }
}
