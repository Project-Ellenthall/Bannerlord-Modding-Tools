namespace TaleWorldsXMLParser.Culture
{
    public static class CultureExtensions
    {
        public static string ToXmlString(this Element element)
        {
            switch(element)
            {
                case Element.clanNames:
                    return "clan names";
                case Element.femaleNames:
                    return "female names";
                default:
                    return "male names";
            }
        }

        public static Element ToElement(this string element)
        {
            switch (element)
            {
                case "clan names":
                    return Element.clanNames;
                case "female names":
                    return Element.femaleNames;
                case "male names":
                    return Element.maleNames;
                default:
                    throw new Framework.BannerlordXmlException("XML Error: Unknown element detected: " + element);
            }
        }

        public static Framework.ICollection NewCollection(this Element element)
        {
            switch (element)
            {
                case Element.clanNames:
                    return new NamesCollection(element.ToXmlString());
                case Element.femaleNames:
                    return new NamesCollection(element.ToXmlString());
                default:
                    return new NamesCollection(element.ToXmlString());
            }
        }
    }
}
