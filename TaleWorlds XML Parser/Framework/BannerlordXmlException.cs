using System;

namespace TaleWorldsXMLParser.Framework
{
    public class BannerlordXmlException : Exception
    {
        public BannerlordXmlException()
        {
            
        }

        public BannerlordXmlException(string message)
            : base(message)
        {

        }

        public BannerlordXmlException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
