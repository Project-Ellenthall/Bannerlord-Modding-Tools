using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaleWorldsXMLParser.Framework
{
    public abstract class AbstractXmlParser
    {
        protected string filepath;
        protected string filename;
        protected string backup;
        protected string xmlDocumentText; // The current text extracted from the XML
        public const string XML_RELATIVE_PATH = "Modules\\SandBoxCore\\ModuleData"; // Exact Path to relative location of XML module data

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
    }


    // This class converts an instance object to a dictionary type object for easy parsing
    public static class ObjectExtensions 
    {
        public static Dictionary<string, object> MapToDictionary(object source)
        {
            var dictionary = new Dictionary<string, object>();
            MapToDictionaryInternal(dictionary, source);
            return dictionary;
        }

        // Internal mapping of objects to deal with nesting of objects and enumerable types and write them to a dictionary
        private static void MapToDictionaryInternal(
            Dictionary<string, object> dictionary, object source)
        {
            var properties = source.GetType().GetProperties();
            foreach (var p in properties)
            {
                var key = p.Name; // Key name in dictionary
                object value = p.GetValue(source, null); // Value of Key (Instance value for class)
                if (value == null) // No present value found in instance
                {
                    dictionary[key] = null;
                    continue; // Bottomed out on parsing
                }
                Type valueType = value.GetType();

                if (valueType == typeof(string) || valueType == typeof(bool)) // Final parsable value type (no nesting)
                {
                    dictionary[key] = value.ToString();
                }
                else if (value is IEnumerable) // A list of objects to parse though (most likely)
                {
                    List<Dictionary<string, object>> local_list = new List<Dictionary<string, object>>(); // Need a new dictionary for nesting (list) (Needs to be an array of dictionaries)
                    dictionary[key] = local_list;
                    foreach (object o in (IEnumerable)value)
                    {
                        var nested_dictionary = new Dictionary<string, object>(); // Need a new dictionary for nesting
                        MapToDictionaryInternal(nested_dictionary, o); // Map out dictionary appropriately
                        local_list.Add(nested_dictionary); // Add child object to list of objects
                    }
                }
                else // Object to parse though (nesting)
                {
                    var nested_dictionary = new Dictionary<string, object>(); // Need a new dictionary for nesting
                    dictionary[key] = nested_dictionary; // Need a new dictionary for nesting (child instances)
                    MapToDictionaryInternal(nested_dictionary, value);
                }
            }
        }
    }
}