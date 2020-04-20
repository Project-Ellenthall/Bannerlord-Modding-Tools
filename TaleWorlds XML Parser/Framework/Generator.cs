using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorldsXMLParser.Framework
{
    public static class Generator
    {

        public static string GenerateString(int length = 9)
        {
            List<string> range = new List<string>();
            string builder = "";

            for (int i = 65; i <= 90; i++)
            {
                range.Add(((char)i).ToString());
            }
            for (int i = 97; i <= 122; i++)
            {
                range.Add(((char)i).ToString());
            }
            for (int i = 0; i <= 9; i++)
            {
                range.Add(i.ToString());
            }
            var result = range.OrderBy(e => Guid.NewGuid()).Take(length).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                builder += result[i];
            }
            return builder;
        }
    }
}
