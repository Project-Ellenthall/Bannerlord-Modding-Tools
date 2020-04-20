namespace TaleWorldsXMLParser.Culture
{
    public static class CultureExtensions
    {
        public static string ToXmlString(this Element element)
        {
            switch(element)
            {
                case Element.clanNames:
                    return "clan_names";
                case Element.femaleNames:
                    return "female_names";
                default:
                    return "male_names";
            }
        }

        public static Element ToElement(this string element)
        {
            switch (element)
            {
                case "clan_names":
                    return Element.clanNames;
                case "female_names":
                    return Element.femaleNames;
                case "male_names":
                    return Element.maleNames;
                default:
                    throw new Framework.BannerlordXmlException("XML Error: Unknown element detected: " + element);
            }
        }

        public static Framework.IXMLCollection NewCollection(this Element element)
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
