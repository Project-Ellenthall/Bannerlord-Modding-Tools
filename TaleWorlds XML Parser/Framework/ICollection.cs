﻿using System.Xml.Linq;

namespace TaleWorldsXMLParser.Framework
{
    public interface ICollection
    {
        void AddNew(object value);
        void AddExisting(string id, object value);
        void Update(string id, object newValue);
        void Delete(string id);
        XElement Save();
    }
}
