using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TaleWorldsXMLParser.Framework;

namespace TaleWorldsXMLParser.Culture
{
    public class CultureParser : Framework.IXMLParser<Element>
    {
        public const string XML_PARSER = "SPCultures";

        protected string filepath;
        protected string filename;
        protected XElement backup;
        protected List<Culture> cultures = new List<Culture>();

        public string AbsolutePath => filepath + filename + ".xml";
        public string BackupPath => filepath + filename + " - Backup.xml";

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filepath"> Path of the file directory.</param>
        /// <param name="filename"> name of the file.</param>
        public CultureParser(string filepath, string filename)
        {
            Initialize(filepath, filename);   
            this.Load(AbsolutePath);
        }

        protected CultureParser()
        {

        }
        #endregion Constructors
        /// <summary>
        /// Method to initialize all variables needed to parse a file.
        /// </summary>
        /// <param name="filepath"> Path of the file directory.</param>
        /// <param name="filename"> name of the file.</param>
        protected void Initialize(string filepath, string filename)
        {
            this.filepath = filepath[filepath.Length - 1] == '\\' ? filepath : filepath + "\\";
            this.filename = filename.Contains(".xml") ? filename.Replace(".xml", "") : filepath;
        }

        #region File Operations
        public void Load(string filepath)
        {
            XElement xmlDocument = XElement.Load(filepath);
            this.backup = xmlDocument;

            foreach (var culture in xmlDocument.Elements())
            {
                cultures.Add(new Culture(culture));
            }
        }
        public void UndoChanges()
        {
            XDocument doc = new XDocument(backup);
            doc.Save(AbsolutePath);
            Load(AbsolutePath);
        }
        public void RestoreBackup()
        {
            Load(BackupPath);
            Save();
        }
        public void Save()
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
        public IXMLObject<Element> Get(string id) => cultures.Find(culture => culture.Id == id);
        /// <summary>
        /// Method to retrieve all cultures.
        /// </summary>
        public List<IXMLObject<Element>> GetAll() => cultures.Select(culture => (IXMLObject<Element>)culture).ToList();
        /// <summary>
        /// Method to update a culture with new data.
        /// </summary>
        /// <param name="xmlObject">The object to update the culture with. </param>
        public void Update(IXMLObject<Element> xmlObject)
        {
            var culture = cultures.Find(c => c.Id == xmlObject.Id);
            culture.Attributes = xmlObject.Attributes;
            culture.Elements = xmlObject.Elements;
        }
        /// <summary>
        /// Method to remove the selected culture.
        /// </summary>
        /// <param name="id">The culture to remove.</param>
        public void Delete(string id) => cultures.RemoveAll(culture => culture.Id == id);
        #endregion Data Operations
    }
}
