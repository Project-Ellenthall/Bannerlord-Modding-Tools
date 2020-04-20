using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaleWorldsXMLParser.Framework
{
    public abstract class AbstractXmlParser<XMLElement> : Framework.IXMLParser<XMLElement> where XMLElement : System.Enum
    {
        protected string filepath;
        protected string filename;
        protected XElement backup;

        public string AbsolutePath => filepath + filename + ".xml";
        public string BackupPath => filepath + filename + " - Backup.xml";

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filepath"> Path of the file directory.</param>
        /// <param name="filename"> name of the file.</param>
        public AbstractXmlParser(string filepath, string filename)
        {
            Initialize(filepath, filename);
            this.Load(AbsolutePath);
        }

        protected AbstractXmlParser()
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
        public abstract void Load(string filepath);
        public abstract void UndoChanges();
        public void RestoreBackup()
        {
            XDocument.Load(BackupPath).Save(AbsolutePath);
        }
        public abstract void Save();
        #endregion File Operations
        #region Data Operations
        /// <summary>
        /// Method to retrieve culture by Id.
        /// </summary>
        /// <param name="id">The id of the culture.</param>
        public abstract IXMLObject<XMLElement> Get(string id);
        /// <summary>
        /// Method to retrieve all cultures.
        /// </summary>
        public abstract List<IXMLObject<XMLElement>> GetAll();
        /// <summary>
        /// Method to update a culture with new data.
        /// </summary>
        /// <param name="xmlObject">The object to update the culture with. </param>
        public abstract void Update(IXMLObject<XMLElement> xmlObject);
        /// <summary>
        /// Method to remove the selected culture.
        /// </summary>
        /// <param name="id">The culture to remove.</param>
        public abstract void Delete(string id);
        #endregion Data Operations
    }
}
