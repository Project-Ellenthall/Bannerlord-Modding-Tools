using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TaleWorldsXMLParser.Culture
{
    public class NamesCollection : Framework.ICollection
    {
        /// <summary>
        /// <para>Key: Id</para>
        /// <para>Value: name value</para>
        /// </summary>
        private Dictionary<string, string> dict;
        private const string xmlName = "name";
        public string CollectionName { get; protected set; }

        public NamesCollection(string collectionName)
        {
            this.dict = new Dictionary<string, string>();
            this.CollectionName = collectionName;
        }


        public List<string> Names => dict.Values.ToList();
        public void AddNew(object name)
        {
            dict.Add("new id", name.ToString());
        }
        public void AddExisting(string id, object name)
        {
            dict.Add(id, name.ToString());
        }
        public void Update(string id, object newValue)
        {
            dict[id] = newValue.ToString();
        }
        public void Delete(string id)
        {
            dict.Remove(id);
        }

        public XElement Save()
        {
            XElement root = new XElement(CollectionName);
            foreach (var value in dict)
            {
                XElement ele = new XElement(xmlName);
                ele.Value = "{" + value.Key + "}" + value.Value;
            }

            return root;
        }
    }
}
