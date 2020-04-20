using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TaleWorldsXMLParser.Framework;

namespace TaleWorldsXMLParser.Culture
{
    public class CultureParser : AbstractXmlParser<Element>
    {
        public const string XML_PARSER = "SPCultures";

        protected List<Culture> cultures = new List<Culture>();

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filepath"> Path of the file directory.</param>
        /// <param name="filename"> name of the file.</param>
        public CultureParser(string filepath, string filename) : base(filepath, filename)
        {
            Initialize(filepath, filename);   
            this.Load(AbsolutePath);
        }

        protected CultureParser()
        {

        }
        #endregion Constructors

        #region File Operations
        /// <summary>
        /// Method to load the xml file at the given path.
        /// </summary>
        /// <param name="filepath"></param>
        public override void Load(string filepath)
        {
            XElement xmlDocument = XElement.Load(filepath);
            this.backup = xmlDocument;

            foreach (var culture in xmlDocument.Elements())
            {
                cultures.Add(new Culture(culture));
            }
        }
        /// <summary>
        /// Method to undo any changes donee to far.
        /// </summary>
        public override void UndoChanges()
        {
            XDocument doc = new XDocument(backup);
            doc.Save(AbsolutePath);
            Load(AbsolutePath);
        }
        /// <summary>
        /// Method to go through each xml element and save to a file.
        /// </summary>
        public override void Save()
        {
            XElement xCultures = new XElement(XML_PARSER);
            foreach(var culture in cultures)
            {
                xCultures.Add(culture.Save());
            }
            XDocument doc = new XDocument(xCultures);
            XDocument backup = new XDocument(this.backup);
            backup.Save(BackupPath);
            doc.Save(AbsolutePath);
        }

        #endregion File Operations
        #region Data Operations
        /// <summary>
        /// Method to retrieve culture by Id.
        /// </summary>
        /// <param name="id">The id of the culture.</param>
        public override IXMLObject<Element> Get(string id) => cultures.Find(culture => culture.Id == id);
        /// <summary>
        /// Method to retrieve all cultures.
        /// </summary>
        public override List<IXMLObject<Element>> GetAll() => cultures.Select(culture => (IXMLObject<Element>)culture).ToList();
        /// <summary>
        /// Method to update a culture with new data.
        /// </summary>
        /// <param name="xmlObject">The object to update the culture with. </param>
        public override void Update(IXMLObject<Element> xmlObject)
        {
            var culture = cultures.Find(c => c.Id == xmlObject.Id);
            culture.Attributes = xmlObject.Attributes;
            culture.Elements = xmlObject.Elements;
        }
        /// <summary>
        /// Method to remove the selected culture.
        /// </summary>
        /// <param name="id">The culture to remove.</param>
        public override void Delete(string id) => cultures.RemoveAll(culture => culture.Id == id);
        #endregion Data Operations
    }
}
