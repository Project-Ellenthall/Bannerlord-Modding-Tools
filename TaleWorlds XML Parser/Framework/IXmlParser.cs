using System.Collections.Generic;

namespace TaleWorldsXMLParser.Framework
{
    public interface IXMLParser<XMLElement> where XMLElement : System.Enum
    {
        void Load(string filepath);
        void UndoChanges();
        void RestoreBackup();
        void Save();
        IXMLObject<XMLElement> Get(string id);
        void Update(IXMLObject<XMLElement> xmlObject);
        List<IXMLObject<XMLElement>> GetAll();
        void Delete(string id);
    }
}
