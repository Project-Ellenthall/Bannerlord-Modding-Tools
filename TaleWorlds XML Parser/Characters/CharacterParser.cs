using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using TaleWorldsXMLParser.Framework;

namespace TaleWorldsXMLParser.Characters
{
    public class CharacterParser : AbstractXmlParser
    {
        public const string CHARACTER_XML = "SPNPCCharacters.xml"; // Name of XML to parse
        protected NPCCharacters npcs; // All NPCs loaded from XSD
        public Dictionary<string, object> npcs_edit; // The modifiable object sent to the UI for easy parsing and feedback

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filepath"> Path of the file directory.</param>
        /// <param name="filename"> name of the file.</param>
        public CharacterParser(string filepath, string filename) : base(filepath, filename)
        {
            Initialize(filepath, filename);
            this.Load(AbsolutePath);
        }

        protected CharacterParser()
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
            this.xmlDocumentText = File.ReadAllText(filepath);
            this.backup = xmlDocumentText;
            XmlSerializer serializer = new XmlSerializer(typeof(NPCCharacters));
            try
            {
                using (StringReader reader = new StringReader(this.xmlDocumentText))
                {
                    this.npcs = (NPCCharacters)(serializer.Deserialize(reader)); // Loads XML into generated class XSD
                    this.npcs_edit = ObjectExtensions.MapToDictionary(this.npcs); // Convert instance to a dictionary
                }
            }
            catch
            {
                throw new BannerlordXmlException("Failed to parse " + CHARACTER_XML);
            }
        }
        /// <summary>
        /// Method to undo any changes donee to far.
        /// </summary>
        public override void UndoChanges()
        {
            // Reload data
        }
        /// <summary>
        /// Method to go through each xml element and save to a file.
        /// </summary>
        public override void Save()
        {
            // Serialize new data
        }
        #endregion File Operations
    }
}
