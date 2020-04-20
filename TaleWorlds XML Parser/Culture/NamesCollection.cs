using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TaleWorldsXMLParser.Framework;

namespace TaleWorldsXMLParser.Culture
{
    public class NamesCollection : Framework.IXMLCollection
    {
        /// <summary>
        /// <para>Key: Id</para>
        /// <para>Value: name value</para>
        /// </summary>
        private Dictionary<string, Text> masterItems;
        private List<string> existingItems;
        private List<string> newItems;
        private const string XML_NAME = "name";
        public string CollectionName { get; protected set; }

        public NamesCollection(string collectionName)
        {
            this.existingItems = new List<string>();
            this.masterItems = new Dictionary<string, Text>();
            this.newItems = new List<string>();
            this.CollectionName = collectionName;
        }


        public List<string> Names => masterItems.Values.Select(text => text.Value).ToList();
        public void AddNew(object name)
        {
            var id = Framework.Generator.GenerateString();
            newItems.Add(id);
            masterItems.Add(id, new Text(id, name.ToString()));
        }
        public void AddExisting(string id, object name)
        {
            existingItems.Add(id);
            masterItems.Add(id, new Text(id, name.ToString()));
        }
        public void Update(string id, object newValue)
        {
            masterItems[id] = new Text(id,newValue.ToString());
        }
        public void Delete(string id)
        {
            masterItems.Remove(id);
            _ = newItems.Contains(id) ? newItems.Remove(id) : existingItems.Remove(id);
        }

        public XElement Save()
        {
            XElement root = new XElement(CollectionName);
            foreach (var text in masterItems.Values)
            {
                XElement element = new XElement(XML_NAME);
                element.SetAttributeValue(XML_NAME, text.ToXmlText());
                root.Add(element);
            }
            //todo: if newItems.Count > 0, then add these values to the string table.
            return root;
        }

       
    }
}
